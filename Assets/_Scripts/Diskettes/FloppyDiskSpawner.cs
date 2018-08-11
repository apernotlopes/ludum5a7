using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloppyDiskSpawner : MonoBehaviour 
{
    public GameObject prefab;
    public int numberToSpawn;
    public int totalFile;
    public float offset;

	void Start () 
	{
        SpawnFloppyDisks();
    }

    void SpawnFloppyDisks () 
	{
        int averageFiles = Mathf.RoundToInt((float)totalFile / (float)numberToSpawn);
        List<FileData> files = new List<FileData>();
        List<Floppy> floppys = new List<Floppy>();

        for (int i = 0; i < totalFile; i++)
            files.Add(new FileData());


        for (int i = 0; i < numberToSpawn; i++)
        {
            Floppy newFloppy = Instantiate(prefab).GetComponent<Floppy>();
            List<FileData> newFloppyFiles = new List<FileData>();

            int flieNum = Mathf.RoundToInt(Random.value * 2.0f * (float)averageFiles);

            for (int j = 0; j < flieNum; j++)
            {
                if (files.Count <= 0) break;

                int rand = Random.Range(0, files.Count);
                newFloppyFiles.Add(files[rand]);
                files.RemoveAt(rand);
            }

            newFloppy.SetFloppy(newFloppyFiles.ToArray());
            newFloppy.transform.position = transform.position + Vector3.up * offset * i;
            newFloppy.transform.rotation = Quaternion.Euler(0, Random.value * 360, 0);
            floppys.Add(newFloppy);
        }

        while(files.Count > 0)
        {
            int rand = Random.Range(0, floppys.Count);
            int rand2 = Random.Range(0, files.Count);

            if (floppys[rand].AddFile(files[rand2]))
            {
                files.RemoveAt(rand2);
            }
        }
	}
}
