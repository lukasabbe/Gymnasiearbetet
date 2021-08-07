using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GridManager{
    public static List<Chunk> chunks = new List<Chunk>();
    public static void InitializeChunks(int chunkLenght, int length, int height){
        for (int x = 0; x < chunkLenght; x++){
            for (int z = 0; z < chunkLenght; z++){
                float yOffset = 0;
                float offset = chunkLenght / 2 * length;
                if (height % 2 == 0) yOffset = -0.5f;
                Vector3 chunkPosition = new Vector3(x * length - 0.5f - offset, height / 2 + yOffset, z * length - 0.5f - offset);
                Vector3 chunkDimensions = new Vector3(length, height, length);

                Chunk chunk = new Chunk(InitializeCells(length, height, x, z, offset), chunkPosition, chunkDimensions);
                chunks.Add(chunk);
            }
        }
    }
    public static List<Cell> InitializeCells(int length, int height, int iterationX, int iterationZ, float offset){
        List<Cell> cells = new List<Cell>();
        for (int x = 0; x < length; x++){
            for (int y = 0; y < height; y++){
                for (int z = 0; z < length; z++){
                    Cell newCell = new Cell(new Vector3(x - (length / 2) + (iterationX * length) - offset, y, z - (length / 2) + (iterationZ * length) - offset));
                    cells.Add(newCell);
                }
            }
        }
        return cells;
    }
    public class Cell{ 
        public bool occupied = false;
        public StructureObject structure = null;

        public Vector3 position = Vector3.zero;
        public Cell(Vector3 _position){
            position = _position;
        }
    }
    public class Chunk{
        public List<Cell> cells = new List<Cell>();

        public Vector3 position = Vector3.zero;
        public Vector3 size = Vector3.zero;
        public Collider collider = new Collider();

        public Chunk(List<Cell> _cells, Vector3 _position, Vector3 _size){
            cells = _cells;
            position = _position;
            size = _size;
        }
    }
    public static void BuildStructure(Vector3 target, StructureObject structure){
        Cell cell = GetCell(target);
        if (structure != null && !cell.occupied){
            cell.occupied = true;

            cell.structure = structure;
            GameObject.Instantiate(cell.structure.gameObject, cell.position, Quaternion.identity);
        }
    }
    public static Chunk GetChunk(Vector3 position){
        int iteration = -1;
        for (int i = 0; i < chunks.Count; i++){
            if (position.x > chunks[i].position.x - (chunks[i].size.x / 2) && position.x < chunks[i].position.x + (chunks[i].size.x / 2)){
                if (position.z > chunks[i].position.z - (chunks[i].size.z / 2) && position.z < chunks[i].position.z + (chunks[i].size.z / 2)){
                    iteration = i;
                }
            }
        }
        if (iteration == -1) return null;
        else return chunks[iteration];
    }
    public static Cell GetCell(Vector3 position){
        Chunk chunk = GetChunk(position);

        int iteration = -1;
        for (int i = 0; i < chunk.cells.Count; i++){
            if (chunk.cells[i].position == position)
                iteration = i;
        }
        if (iteration == -1) return null;
        else return chunk.cells[iteration];
    }
}


