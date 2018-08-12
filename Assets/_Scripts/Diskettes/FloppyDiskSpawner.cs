using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloppyDiskSpawner : MonoBehaviour 
{
    public GameObject prefab;
    [Range(0.0f, 1.0f)] public float floppyFilesRatio;
    public float offset;

    int numberOfFloppy;
    int numberOfFile;

    public void SpawnFloppyDisks () 
	{
        FileGenerator fileGen = FileGenerator.instance;

        List<FileData> files = new List<FileData>();
        List<Floppy> floppys = new List<Floppy>();

        numberOfFloppy = fileGen.categories.Length;
        numberOfFile = Mathf.RoundToInt((float)fileGen.Lenght * floppyFilesRatio);

        int averageFiles = Mathf.RoundToInt((float)numberOfFile / (float)numberOfFloppy);

        for (int i = 0; i < numberOfFloppy; i++)
        {
            Floppy newFloppy = Instantiate(prefab, transform).GetComponent<Floppy>();
            newFloppy.SetFloppy(i);
            newFloppy.transform.position = transform.position + Vector3.up * offset * i;
            newFloppy.transform.rotation = Quaternion.Euler(0, Random.value * 360, 0);
            floppys.Add(newFloppy);
        }

        int safeInt = 0;

        while(numberOfFile > 0)
        {
            int rand = Random.Range(0, floppys.Count);
            int rand2 = Random.Range(0, fileGen.dataToSpawn.Count);

            if (floppys[rand].AddFile(fileGen.dataToSpawn[rand2]))
            {
                fileGen.dataToSpawn.RemoveAt(rand2);
                numberOfFile--;
            }

            safeInt++;
            if (safeInt >= 1000) break;        
        }
	    
	    Debug.Log(PCManager.Instance);
		Debug.Log(PCManager.Instance.hardDrive);
		Debug.Log(PCManager.Instance.hardDrive.Files);

	    foreach (FileData d in fileGen.dataToSpawn)
	    {
	        if(d != null)
	            PCManager.Instance.hardDrive.Files.Add(d);
	    }

        fileGen.dataToSpawn.Clear();

        PCManager.Instance.hardDrive.capacity = Mathf.RoundToInt(PCManager.Instance.hardDrive.GetUsedSpace() * 1.5f);
        PCManager.Instance.initialCapacity = PCManager.Instance.hardDrive.capacity;
    }
}
