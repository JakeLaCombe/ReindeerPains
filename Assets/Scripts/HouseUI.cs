using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HouseUI : MonoBehaviour
{
    public TextMeshProUGUI VaccineCount;
    public TextMeshProUGUI CovidPillCount;
    public TextMeshProUGUI RoosterDecoyCount;
    public TextMeshProUGUI GasTrapCount;

    void Start()
    {
        VaccineCount = this.transform.Find("VaccineCount").GetComponent<TextMeshProUGUI>();
        CovidPillCount = this.transform.Find("COVIDPillCount").GetComponent<TextMeshProUGUI>();
        RoosterDecoyCount = this.transform.Find("DecoyBirdCount").GetComponent<TextMeshProUGUI>();
        GasTrapCount = this.transform.Find("GasTrapCount").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        VaccineCount.text = Supplies.instance.vaccines.ToString();
        CovidPillCount.text = Supplies.instance.covidPills.ToString();
        RoosterDecoyCount.text = Supplies.instance.roosterDecoys.ToString();
        GasTrapCount.text = Supplies.instance.smokeTraps.ToString();
    }
}
