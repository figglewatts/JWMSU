using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeImageAPI;
using System.ComponentModel;

namespace JWMSU.WAD
{
	/// <summary>
	/// A class to load/save WAD3 files.
	/// Heavily based on https://github.com/yuraj11/HL-Texture-Tools/blob/master/HL%20Texture%20Tools/HLTools/WAD3Loader.cs
	/// </summary>
	public class WADLoader
	{
		public List<WADLump> LumpsInfo { get; private set; }
		public string Filename { get; private set; }

		private const int MaxPaletteColors = 256;
		private const int MaxNameLength = 16;
		private const int LumpSize = 32;
		private const int MaxTextureWidth = 4096;
		private const int MaxTextureHeight = 4096;
		private const int QCharWidth = 16;
		private const int QNumbOfGlyphs = 256;
		private readonly static byte[] WadHeaderId = { 0x57, 0x41, 0x44, 0x33 }; //WAD3

		private WADHeader header;
		private BinaryReader binReader;
		private FileStream fs;

		private long palleteBlockPos = 0;
		private long pixelsBlockPos = 0;
		private long lastImageSize = 0;
		private long lastImageWidth = 0;
		
		public struct WADHeader
		{
			public char[] Id; // 4 chars, WAD3
			public uint LumpCount;
			public uint LumpOffset;
		}

		public struct WADLump
		{
			public uint Offset;
			public uint CompressedLength;
			public uint FullLength;
			public byte Type;
			public byte Compression;
			public string Name;

			public override string ToString()
			{
				return Name;
			}
		}

		public class Texture
		{
			public Bitmap Image;
			public TextureMipmaps Mipmaps;

			public class TextureMipmaps
			{
				public byte[] Mipmap1;
				public byte[] Mipmap2;
				public byte[] Mipmap3;
			}
		}

		public struct CharInfo
		{
			public ushort StartOffset;
			public ushort CharWidth;

			public override string ToString()
			{
				return string.Format("Offset: {0:X}, Width: {1:X}", StartOffset, CharWidth);
			}
		}

		public WADLoader()
		{
			LumpsInfo = new List<WADLump>();
		}

		public void Close()
		{
			if (binReader != null)
			{
				binReader.Close();
			}
			if (fs != null)
			{
				fs.Close();
			}
		}

		private static byte[] CreateTextureName(string text)
		{
			byte[] newName = new byte[MaxNameLength];
			byte[] b = System.Text.Encoding.ASCII.GetBytes(text);
			b.CopyTo(newName, 0);

			newName[MaxNameLength-1] = 0;
			return newName;
		}

		public static bool CreateWad(string outputFilename, string[] images, string[] names,
			bool reserverLastPalColor = false)
		{
			if (images.Length <= 0) { return false; }
			
			using (FileStream fs = new FileStream(outputFilename, FileMode.Create))
			using (BinaryWriter bw = new BinaryWriter(fs))
			{
				// convert bitmaps to 8bpp format
				List<FreeImageBitmap> imgs = new List<FreeImageBitmap>();
				for (int i = 0; i < images.Length; i++)
				{
					string image = images[i];

					if (!File.Exists(image)) { return false; }

					// quantize images
					FreeImageBitmap originalImage = FreeImageBitmap.FromFile(image);
					//originalImage.IsTransparent = true;

					//originalImage.SwapColors(new RGBQUAD(Color.Transparent), new RGBQUAD(Color.Blue), true);

					// if texture will be transparent, reserve last colour if enabled
					bool reserveLastClr = (names[i].StartsWith("{") && reserverLastPalColor);
					int r = reserveLastClr ? 1 : 0;
					originalImage.Quantize(FREE_IMAGE_QUANTIZE.FIQ_NNQUANT, MaxPaletteColors - r);
					originalImage.ConvertColorDepth(FREE_IMAGE_COLOR_DEPTH.FICD_08_BPP);

					if (reserveLastClr) originalImage.Palette[MaxPaletteColors - 1] = new RGBQUAD(Color.Blue);

					imgs.Add(originalImage);
				}
				uint[] offsets = new uint[images.Length];
				uint[] sizes = new uint[images.Length];

				// WAD header
				bw.Write(WadHeaderId);
				bw.Write(images.Length);
				bw.Write(0);

				// write textures
				for (int i = 0; i < images.Length; i++)
				{
					uint posTextureStart = (uint)bw.BaseStream.Position;
					offsets[i] = posTextureStart;
					// texture name
					byte[] name = CreateTextureName(names[i]);
					bw.Write(name, 0, name.Length);

					// texture dimensions
					bw.Write(imgs[i].Width);
					bw.Write(imgs[i].Height);

					// offsets
					uint posImage = (uint)(bw.BaseStream.Position - posTextureStart);
					bw.Write(posImage + 16); // image
					int pixelSize = ((imgs[i].Width) * (imgs[i].Height));
					int m1 = ((imgs[i].Width / 2) * (imgs[i].Height / 2));
					int m2 = ((imgs[i].Width / 4) * (imgs[i].Height / 4));
					int m3 = ((imgs[i].Width / 8) * (imgs[i].Height / 8));
					bw.Write((uint)(posImage + pixelSize + 16)); // mipmap1
					bw.Write((uint)(posImage + pixelSize + m1 + 16)); // mipmap2
					bw.Write((uint)(posImage + pixelSize + m1 + m2 + 16)); // mipmap3

					// write pixel data
					imgs[i].RotateFlip(RotateFlipType.RotateNoneFlipX);
					byte[] arr = new byte[imgs[i].Width * imgs[i].Height];
					System.Runtime.InteropServices.Marshal.Copy(imgs[i].GetScanlinePointer(0),
						arr, 0, arr.Length);
					Array.Reverse(arr);
					bw.Write(arr);

					// mipmap data
					int factor = 2;
					for (int a = 0; a < 3; a++)
					{	
						int widthMM = (imgs[i].Width / factor);
						int heightMM = (imgs[i].Height / factor);

						using (FreeImageBitmap clBmp = imgs[i].GetScaledInstance(widthMM, heightMM, FREE_IMAGE_FILTER.FILTER_LANCZOS3))
						{
							clBmp.Quantize(FREE_IMAGE_QUANTIZE.FIQ_NNQUANT, MaxPaletteColors, imgs[i].Palette);
							byte[] arrMM = new byte[widthMM * heightMM];
							System.Runtime.InteropServices.Marshal.Copy(clBmp.GetScanlinePointer(0), arrMM, 0,
								arrMM.Length);
							Array.Reverse(arrMM);
							bw.Write(arrMM);
						}
						factor *= 2;
					}

					// unknown 2 bytes
					bw.Write(new byte[] { 0x00, 0x01 });

					// write color palette
					for (int p = 0; p < imgs[i].Palette.Length; p++)
					{
						bw.Write(imgs[i].Palette[p].rgbRed);
						bw.Write(imgs[i].Palette[p].rgbGreen);
						bw.Write(imgs[i].Palette[p].rgbBlue);
					}

					// padding
					bw.Write(new byte[] { 0x00, 0x00 });
					sizes[i] = (uint)bw.BaseStream.Position - posTextureStart;
				}

				long posLumps = bw.BaseStream.Position;
				bw.Seek(8, SeekOrigin.Begin);
				bw.Write((uint)posLumps);
				bw.Seek((int)posLumps, SeekOrigin.Begin);

				// write lumps info
				for (int i = 0; i < images.Length; i++)
				{
					bw.Write(offsets[i]);
					bw.Write(sizes[i]);
					bw.Write(sizes[i]);
					bw.Write((byte)0x43);
					bw.Write((byte)0);
					bw.Write(new byte[] { 0x00, 0x00 });
					byte[] name = CreateTextureName(names[i]);
					bw.Write(name, 0, name.Length);
				}

				// free resources
				for (int i = 0; i < imgs.Count; i++)
				{
					imgs[i].Dispose();
				}
				return true;
			}
		}
	}
}
