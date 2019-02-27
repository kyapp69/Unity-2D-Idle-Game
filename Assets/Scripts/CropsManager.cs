using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropsManager : MonoBehaviour {

	public CoinManager coinManager = null;// Only passes to CropItemState
	public VerticalLayoutGroup layoutGroup = null;
	public GameObject cropItemBarPrefab = null;
	public GameObject winText = null;
	public ParticleSystem winFireworks = null;

	private int currentCropIndex = -1;
	private readonly List<Crop> CROPS_LIST = new List<Crop>();
	private readonly string[] CROPS =
		{"Carrot", "Beets", "Aubergine", "Lemon", "Tomato", "Pineapple", "Cucumber", "Banana", "Bear", "3 REALLY BIG Beans", "One normal bean", "Legit actual magic bean", "You can stop playing now"};

	private SortedList<int, CropItemStateHandler> cropItemStateHandlers = new SortedList<int, CropItemStateHandler>();


	// Save
	public CropsManagerData SaveData() {
		// Saves amount of each crop as [int cropIndex, int amount]
		SortedList<int, int> cropsCount = new SortedList<int, int>();
		int index = 0;
		foreach (CropItemStateHandler cropItemStateHandler in cropItemStateHandlers.Values) {
			cropsCount.Add(index, cropItemStateHandler.amount);
			index++;
		}
		
		return new CropsManagerData(cropsCount);
	}

	// Load
	public void LoadData(CropsManagerData cropsManagerData) {
		PopulateCropsList();
		SortedList<int, int> cropsCount = cropsManagerData.cropsCount;
		for (int i = 0; i < cropsCount.Count; i++) {
			currentCropIndex++;
			int amount = cropsCount.Values[i];
			InstantiateCropItemBar(CROPS_LIST[i], amount);
		}
	}

	// Like Start() or LoadData() when there is no save file
	public void StartNewGame() {
		PopulateCropsList();
		UnlockNextCrop();
	}

	private void PopulateCropsList() {
		int value = 1;
		for (int i = 0; i < CROPS.Length; i++) {
			CROPS_LIST.Add(new Crop(CROPS[i], value));
			value = value * 4;
		}
	}

	public void UnlockNextCrop() {
		currentCropIndex++;
		if (currentCropIndex < CROPS_LIST.Count) {
			InstantiateCropItemBar(CROPS_LIST[currentCropIndex]);
		} else {
			winText.SetActive(true);
			winFireworks.Play();
		}
	}

	private void InstantiateCropItemBar(Crop crop, int amount = 0) {
		GameObject cropItemBar = Instantiate(cropItemBarPrefab, layoutGroup.transform);

		CropItemStateHandler cropItemStateHandler = cropItemBar.AddComponent<CropItemStateHandler>();
		cropItemStateHandler.Initialise(crop, coinManager);

		if (amount != 0) {
			cropItemStateHandler.LoadData(amount);
		}

		// add crop state handler to array - to get data from each crop later for SAVING game data
		cropItemStateHandlers.Add(currentCropIndex, cropItemStateHandler);
	}

	private void LoadCropData() {

	}
}
