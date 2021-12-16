using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialsUI : MonoBehaviour
{
    public TextMeshProUGUI VaccineCount;
    public TextMeshProUGUI ReindeerPillCount;
    public TextMeshProUGUI CovidPillCount;

    // Start is called before the first frame update
    void Awake()
    {
        VaccineCount = this.transform.Find("Vaccine Count").GetComponent<TextMeshProUGUI>();
        ReindeerPillCount = this.transform.Find("Reindeer Pill Count").GetComponent<TextMeshProUGUI>();
        CovidPillCount = this.transform.Find("COVID Pill Count").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        VaccineCount.text = "x " + Supplies.instance.vaccines;
        ReindeerPillCount.text = "x " + Supplies.instance.reindeerPills;
        CovidPillCount.text = "x " + Supplies.instance.covidPills;
    }
}
