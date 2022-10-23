using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ACSharp;
using ACSharp.ResourceTypes;
using System.IO;
using Force.Crc32;

namespace ACSharpTests {
	class Program {
		static void Main(string[] args) {

			byte[] hashbytes = Encoding.ASCII.GetBytes("GridCellDataBlock");
			uint hash = Crc32Algorithm.Compute(hashbytes, 0, hashbytes.Length);
			Console.WriteLine(hash);

			hashbytes = Encoding.ASCII.GetBytes("DataBlock");
			hash = Crc32Algorithm.Compute(hashbytes, 0, hashbytes.Length);
			Console.WriteLine(hash);

			//AC2FileTypeTest();
			//Forge f = new Forge(@"E:\Games\Ubisoft Game Launcher\games\Assassin's Creed II\DataPC_Toscana.forge", Games.AC2);
			//Console.WriteLine(f.datafileTable[0].name);
			//AC2IDTest();
			//AC2RewriteTest();
			//Console.WriteLine(args.Length);
			//for (int i = 0; i < args.Length; i++) Console.WriteLine(args[i]);
			//if (args.Length != 1) return;
			//if (!Directory.Exists(args[0])) return;
			//string path = Path.Combine(args[0], "DataPC_Venezia.forge");
			//if (File.Exists(path)) AC2RewriteTest(path);
		}

		static void AC2FileTypeTest() {
			Dictionary<uint, string> hashes = new Dictionary<uint, string>();
			foreach (string line in File.ReadAllLines(@"E:\Anna\Desktop\FORGEd.txt")) {
				byte[] hashbytes = Encoding.ASCII.GetBytes(line);
				uint hash = Crc32Algorithm.Compute(hashbytes, 0, hashbytes.Length);
				if (!hashes.ContainsKey(hash)) hashes[hash] = line;
				else Console.WriteLine($"COLLISION {line} - {hashes[hash]}");
			}


			Dictionary<uint, int> typeCounts = new Dictionary<uint, int>();
			foreach(string path in Directory.EnumerateFiles(@"E:\Games\Ubisoft Game Launcher\games\Assassin's Creed II\", "*.forge")) {
				//if (path.Contains("extra") || path.Contains("Firenze")) continue;

				Forge f = new Forge(path, Games.AC2);
				for (int i = 0; i < f.datafileTable.Length; i++) {
					ForgeFile[] files = f.OpenDatafile(i, null, false);
					if (files is null) {
						Console.WriteLine(Path.GetFileName(path) + " " + f.datafileTable[i].name + " do not worky");
						continue;
					}
					for (int file = 0; file < files.Length; file++) {
						if (!typeCounts.ContainsKey(files[file].fileType)) typeCounts[files[file].fileType] = 1;
						else typeCounts[files[file].fileType] += 1;
					}
				}
			}
			foreach (uint type in typeCounts.Keys) {
				if(hashes.ContainsKey(type)) Console.WriteLine($"{hashes[type]}: {typeCounts[type]}");
				else Console.WriteLine($"UNK{type}: {typeCounts[type]}");
			}
		}


		static void AC2IDTest() {
			Dictionary<uint, ForgeFile> fileDict = new Dictionary<uint, ForgeFile>();
			foreach (string forgePath in Directory.EnumerateFiles(@"F:\Games\Assassin's Creed II\", "*.forge")) {
				Forge f = new Forge(forgePath, Games.AC2);
				for (int i = 0; i < f.datafileTable.Length; i++) f.OpenDatafile(i, fileDict);
			}
			StreamWriter writer = new StreamWriter(File.Open("IDLIST.txt", FileMode.Create));
			foreach (uint id in fileDict.Keys) writer.WriteLine($"{id}\t{fileDict[id].name}\t{fileDict[id].fileType}\t{fileDict[id].forge.name}");
			writer.Flush();
			writer.Close();

			//Forge f = new Forge(@"F:\Games\Assassin's Creed II\DataPC_Venezia.forge", Games.AC2);
			//f.SetModified();
			//f.Revert();
			//Forge f2 = new Forge(@"F:\Games\Assassin's Creed II\DataPC_Firenze.forge", Games.AC2);
			//Dictionary<uint, ForgeFile> fileDict = new Dictionary<uint, ForgeFile>();
			//for (int i = 0; i < f.datafileTable.Length; i++) f.OpenDatafile(i, fileDict);
			//for (int i = 0; i < f2.datafileTable.Length; i++) f2.OpenDatafile(i, fileDict);
		}

		static void AC2UnlockCemetery(string path = @"F:\Games\Assassin's Creed II\DataPC_Venezia.forge") {
			Forge f = new Forge(path, Games.AC2);

			f.SetModified();
			int id169 = f.IndexOf("Cell00169");
			byte[] cell169 = f.DecompressDatafile(id169);
			byte[] replace169 = BitConverter.GetBytes((uint)2020722711);
			Array.Copy(replace169, 0, cell169, 340, 4);
			f.RewriteDatafile(id169, cell169);

			int id170 = f.IndexOf("Cell00170");
			byte[] cell170 = f.DecompressDatafile(id170);
			byte[] replace170 = BitConverter.GetBytes((uint)2020722712);
			Array.Copy(replace170, 0, cell170, 370, 4);
			Array.Copy(replace170, 0, cell170, 376, 4);
			f.RewriteDatafile(id170, cell170);

			int id171 = f.IndexOf("Cell00171");
			byte[] cell171 = f.DecompressDatafile(id171);
			byte[] replace171 = BitConverter.GetBytes((uint)2020722713);
			Array.Copy(replace171, 0, cell171, 340, 4);
			f.RewriteDatafile(id171, cell171);

			int id172 = f.IndexOf("Cell00172");
			byte[] cell172 = f.DecompressDatafile(id172);
			byte[] replace172 = BitConverter.GetBytes((uint)2020722714);
			Array.Copy(replace172, 0, cell172, 340, 4);
			f.RewriteDatafile(id172, cell172);

			int id173 = f.IndexOf("Cell00173");
			byte[] cell173 = f.DecompressDatafile(id173);
			byte[] replace173 = BitConverter.GetBytes((uint)2020722715);
			Array.Copy(replace173, 0, cell173, 340, 4);
			f.RewriteDatafile(id173, cell173);

			int id174 = f.IndexOf("Cell00174");
			byte[] cell174 = f.DecompressDatafile(id174);
			byte[] replace174 = BitConverter.GetBytes((uint)2020722716);
			Array.Copy(replace174, 0, cell174, 340, 4);
			f.RewriteDatafile(id174, cell174);

			int id175 = f.IndexOf("Cell00175");
			byte[] cell175 = f.DecompressDatafile(id175);
			byte[] replace175 = BitConverter.GetBytes((uint)2020722717);
			Array.Copy(replace175, 0, cell175, 340, 4);
			f.RewriteDatafile(id175, cell175);

			int id176 = f.IndexOf("Cell00176");
			byte[] cell176 = f.DecompressDatafile(id176);
			byte[] replace176 = BitConverter.GetBytes((uint)2020722718);
			Array.Copy(replace176, 0, cell176, 340, 4);
			f.RewriteDatafile(id176, cell176);

			int id177 = f.IndexOf("Cell00177");
			byte[] cell177 = f.DecompressDatafile(id177);
			byte[] replace177 = BitConverter.GetBytes((uint)2020722719);
			Array.Copy(replace177, 0, cell177, 439, 4);
			Array.Copy(replace177, 0, cell177, 445, 4);
			Array.Copy(replace177, 0, cell177, 451, 4);
			f.RewriteDatafile(id177, cell177);

			int id198 = f.IndexOf("Cell00198");
			byte[] cell198 = f.DecompressDatafile(id198);
			byte[] replace198 = BitConverter.GetBytes((uint)2020722740);
			Array.Copy(replace198, 0, cell198, 340, 4);
			f.RewriteDatafile(id198, cell198);

			int id199 = f.IndexOf("Cell00199");
			byte[] cell199 = f.DecompressDatafile(id199);
			byte[] replace199 = BitConverter.GetBytes((uint)2020722741);
			Array.Copy(replace199, 0, cell199, 340, 4);
			f.RewriteDatafile(id199, cell199);

			int id200 = f.IndexOf("Cell00200");
			byte[] cell200 = f.DecompressDatafile(id200);
			byte[] replace200 = BitConverter.GetBytes((uint)2020722742);
			Array.Copy(replace200, 0, cell200, 340, 4);
			f.RewriteDatafile(id200, cell200);

			/*
			SaveDatafile(f, "Cell00198");
			SaveDatafile(f, "Cell00199");
			SaveDatafile(f, "Cell00200");
			SaveDatafile(f, "Cell00169");
			SaveDatafile(f, "Cell00170");
			SaveDatafile(f, "Cell00171");
			SaveDatafile(f, "Cell00172");
			SaveDatafile(f, "Cell00173");
			SaveDatafile(f, "Cell00174");
			SaveDatafile(f, "Cell00175");
			SaveDatafile(f, "Cell00176");
			SaveDatafile(f, "Cell00177");
			*/
			//byte[] cell171 = f.DecompressDatafile(283);
			//File.WriteAllBytes($"{f.datafileTable[283].name}.data", cell171);
			//byte[] cell171bytes = BitConverter.GetBytes((uint)2020722713);
			//Array.Copy(cell171bytes, 0, cell171, 340, 4);
			//f.RewriteDatafile(283, cell171);

			//byte[] cell200 = f.DecompressDatafile(284);
			//File.WriteAllBytes($"{f.datafileTable[284].name}.data", cell171);
			//byte[] cell200bytes = BitConverter.GetBytes((uint)2020722713);
			//Array.Copy(cell200bytes, 0, cell200, 340, 4);
			//f.RewriteDatafile(284, cell200);


			//byte[] cell172 = f.DecompressDatafile(396);
			//File.WriteAllBytes($"{f.datafileTable[396].name}.data", uncompressedData);
			//byte[] cell172bytes = BitConverter.GetBytes((uint)2020722714);
			//Array.Copy(cell172bytes, 0, cell172, 340, 4);
			//f.RewriteDatafile(396, cell172);

			/*
			byte[] newPos = BitConverter.GetBytes(1000f);
			Array.Copy(newPos, 0, uncompressedData, 22540, 4);
			Array.Copy(newPos, 0, uncompressedData, 22544, 4);
			Array.Copy(newPos, 0, uncompressedData, 22548, 4);


			byte[] oobdata = File.ReadAllBytes(@"F:\Media Libraries\Anna\Files\Unity\ACSharpTests\bin\Release\testoobdata.data");
			Console.WriteLine("OOB " + oobdata.Length);
			Array.Copy(oobdata, 0, uncompressedData, 22587, 448);

			byte[] meshShapeData = File.ReadAllBytes(@"F:\Media Libraries\Anna\Files\Unity\ACSharpTests\bin\Release\testmeshshapedata.data");
			Console.WriteLine("MESHSHAPE " + meshShapeData.Length);
			Array.Copy(meshShapeData, 0, uncompressedData, 24295, meshShapeData.Length);

			Array.Copy(newPos, 0, uncompressedData, 22599, 4);
			Array.Copy(newPos, 0, uncompressedData, 22663, 4);
			Array.Copy(newPos, 0, uncompressedData, 22727, 4);
			Array.Copy(newPos, 0, uncompressedData, 22791, 4);
			Array.Copy(newPos, 0, uncompressedData, 22855, 4);
			Array.Copy(newPos, 0, uncompressedData, 22919, 4);
			Array.Copy(newPos, 0, uncompressedData, 22983, 4);
			*/



			/*
			byte[] uncompressedData = f.DecompressDatafile(412);

			byte[] newPos = BitConverter.GetBytes(14.5f);
			Array.Copy(newPos, 0, uncompressedData, 771072, 4);

			Console.WriteLine(uncompressedData.Length);
			f.RewriteDatafile(412, uncompressedData);
			*/
			//File.WriteAllBytes($"{ f.datafileTable[412].name}.data", uncompressedData);
		}

		static void SaveDatafile(Forge f, string start) {
			int i = f.IndexOf(start);
			byte[] data = f.DecompressDatafile(i);
			File.WriteAllBytes($"{f.datafileTable[i].name}.data", data);
		}

		static void AC2Test2() {
			Forge f = new Forge(@"F:\Games\Assassin's Creed II\DataPC_Toscana.forge", Games.AC2);
			SaveDatafile(f, "Cell00626");
			//Dictionary<uint, ForgeFile> fileDict = new Dictionary<uint, ForgeFile>();
			//for (int i = 0; i < f.datafileTable.Length; i++) f.OpenDatafile(i, fileDict);
			//foreach (ForgeFile file in fileDict.Values) {
			//	if (file.fileType != Entity.id) continue;
			//	Entity e = (Entity)file.resource;
			//	if(e.visual != null) {
			//		if(e.visual.meshInstanceData != null) Console.WriteLine($"{file.name}: {e.visual.meshInstanceData.meshID}");
			//		else if(e.visual.lodSelectorInstance != null && e.visual.lodSelectorInstance.lod0 != null) Console.WriteLine($"{file.name}: {e.visual.lodSelectorInstance.lod0.meshID}");
			//	} else Console.WriteLine($"{file.name} NO FOUND VISUAL");
			//}
		}

		static void AC2Tests() {
			Forge f = new Forge(@"F:\Games\Assassin's Creed II\DataPC_Toscana.forge", Games.AC2);
			ForgeFile[] files = f.OpenDatafile(2);
			for (int i = 0; i < files.Length; i++) {
				if (files[i].fileType == Mesh.id) {
					Mesh mesh = (Mesh)files[i].resource;
					if (mesh.compiledMesh != null) Console.WriteLine($"{files[i].name} - {mesh.compiledMesh.verts.Length} - {mesh.compiledMesh.idx.Length / 3}");

				} else Console.WriteLine(files[i].name);

				//}
				//byte[] data = f.DecompressDatafile(1);
				//File.WriteAllBytes("toscanatest.ac2", data);
				//Console.WriteLine(files.Length);
				//for (int i = 0; i < f.datafileTable.Length; i++) Console.WriteLine(f.datafileTable[i].name);
			}
		}

		static void NewEntityTest() {
			Forge f = new Forge(@"F:\Games\Assassin's Creed 1\DataPC_Damascus.forge");
			Dictionary<uint, ForgeFile> fileDict = new Dictionary<uint, ForgeFile>();
			for (int i = 0; i < f.datafileTable.Length; i++) f.OpenDatafile(i, fileDict);
			//foreach (ForgeFile file in fileDict.Values) {
			//	if (file.fileType != Entity.id) continue;
			//	Console.WriteLine($"{file.name}: {((Entity)file.resource).entities.Count}");
			//}
		}

		static void ExportTestsNew() {
			Forge f = new Forge(@"F:\Games\Assassin's Creed 1\DataPC_Damascus.forge");
			ForgeFile[] files = f.OpenDatafile(233);
			foreach(ForgeFile file in files) {
				if(file.name == "Chest_Group_01a_042") {
					GameStateData g = ((Entity)file.resource).entityDescriptor.gameStateData;
					for (int i = 0; i < 16; i++) Console.WriteLine(g.transformationMatrix[i]);
					//Forge.EntityEditEntry editEntry = new Forge.EntityEditEntry() { datafile = 233, datafileOffset = file.datafileOffset, transformOffset = g.transformationMatrixOffset, x = g.transformationMatrix[12], y = g.transformationMatrix[13], z = g.transformationMatrix[14] + 4 };
					Forge.EntityEditEntry edit = new Forge.EntityEditEntry() { datafile = 691117264, datafileOffset = 1147835, transformOffset = 15886, x = 75f, y = -14.42f, z = 18.62f };
					f.EditEntity(edit);
					break;
				}
			}
		}

		static void ExportTests() {
			Forge f = new Forge(@"F:\Games\Assassin's Creed 1\DataPC_Damascus.forge");

			byte[] uncompressedData = f.DecompressDatafile(233);
			File.WriteAllBytes($"{f.datafileTable[233].name}.data", uncompressedData);
			//byte[] newPos = BitConverter.GetBytes(6.5f);
			//Array.Copy(newPos, 0, uncompressedData, 164613, 4);
			//Array.Copy(newPos, 0, uncompressedData, 1214616, 4);
			//f.RewriteDatafile(186, uncompressedData);

			/*
			byte[] uncompressedData = f.DecompressDatafile(233);
			byte[] newPos = BitConverter.GetBytes(14f);
			//File.WriteAllBytes($"{f.datafileTable[233].name}.data", uncompressedData);
			Array.Copy(newPos, 0, uncompressedData, 1148271, 4);
			Array.Copy(newPos, 0, uncompressedData, 1214616, 4);
			f.RewriteDatafile(233, uncompressedData);
			*/
		}

		static void PackTests() {
			Forge f = new Forge(@"F:\Games\Assassin's Creed 1\DataPC.forge");
			Dictionary<uint, ForgeFile> fileDict = new Dictionary<uint, ForgeFile>();
			for (int i = 0; i < f.datafileTable.Length; i++) f.OpenDatafile(i, fileDict);
		}

		static void EntityGroupTest() {
			Forge f = new Forge(@"F:\Games\Assassin's Creed 1\DataPC_Jerusalem.forge");
			Dictionary<uint, ForgeFile> fileDict = new Dictionary<uint, ForgeFile>();
			for (int i = 0; i < f.datafileTable.Length; i++) f.OpenDatafile(i, fileDict);
			foreach (ForgeFile file in fileDict.Values) {
				if (file.fileType != EntityGroup.id) continue;
				Console.WriteLine($"{file.name}: {((EntityGroup)file.resource).entities.Count}");
			}
		}

		static void MeshTest() {
			Forge f = new Forge(@"F:\Games\Assassin's Creed 1\DataPC_Jerusalem.forge");
			Dictionary<uint, ForgeFile> fileDict = new Dictionary<uint, ForgeFile>();
			for (int i = 0; i < f.datafileTable.Length; i++) f.OpenDatafile(i, fileDict);
			foreach (ForgeFile file in fileDict.Values) {
				if (file.fileType != Mesh.id) continue;
				Mesh mesh = (Mesh)file.resource;
				if(mesh.compiledMesh == null) Console.WriteLine($"{file.name} COULD NOT FIND COMPILED MESH");
				else Console.WriteLine($"{file.name} {mesh.compiledMesh.verts.Length}");
				//for (int i = 0; i < mesh.compiledMesh.verts.Length; i++) {
				//	float[] pos = mesh.compiledMesh.verts[i].getPosition();
				//	Console.WriteLine($"{pos[0]},{pos[1]},{pos[2]}");
				//}
				//break;
			}
		}

		static void DataBlockTest() {
			Forge f = new Forge(@"F:\Games\Assassin's Creed 1\DataPC_SolomonTemple.forge");
			Dictionary<uint, ForgeFile> fileDict = new Dictionary<uint, ForgeFile>();
			for (int i = 0; i < f.datafileTable.Length; i++) f.OpenDatafile(i, fileDict);
			foreach (ForgeFile file in fileDict.Values) {
				if (file.fileType != DataBlock.id) continue;
				Console.WriteLine($"{file.name}");
				DataBlock dataBlock = (DataBlock)file.resource;
				for (int j = 0; j < dataBlock.files.Length; j++) {
					if (!fileDict.ContainsKey(dataBlock.files[j])) continue;
					ForgeFile dblockFile = fileDict[dataBlock.files[j]];
					if (dblockFile.fileType != Entity.id) continue;
					Console.Write(dblockFile.name);

					Entity entity = (Entity)dblockFile.resource;
					if(entity.visual != null) {
						if (entity.visual.meshInstanceData != null) Console.Write($" {entity.visual.meshInstanceData.meshID}");
						else if (entity.visual.lodSelectorInstance != null && entity.visual.lodSelectorInstance.lod0 != null) Console.Write($" {entity.visual.lodSelectorInstance.lod0.meshID}");
					}
					//for (int h = 0; h < 16; h++) Console.Write(" " + ((Entity)dblockFile.resource).transformationMatrix[h]);
					Console.WriteLine();
				}
				Console.WriteLine();
			}
			/*
			ForgeFile[] files = f.OpenDatafile(15, fileDict);
			for (int i = 0; i < files.Length; i++) {
				if (files[i].fileType == DataBlock.id) {
					Console.WriteLine($"{files[i].name}");
					DataBlock dataBlock = (DataBlock)files[i].resource;
					for (int j = 0; j < dataBlock.files.Length; j++) {
						if(fileDict.ContainsKey(dataBlock.files[j])) Console.WriteLine($"{fileDict[dataBlock.files[j]].name} - {dataBlock.files[j]}");
						else Console.WriteLine(dataBlock.files[j]);
					}
				}
			}
			*/
		}
	}
}
