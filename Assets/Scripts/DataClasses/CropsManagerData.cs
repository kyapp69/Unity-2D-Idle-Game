using System.Collections.Generic;

[System.Serializable]
public class CropsManagerData {

    public SortedList<int, int> cropsCount;

    public CropsManagerData(SortedList<int, int> cropsCount) {
        this.cropsCount = cropsCount;
    }

}