using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MaterialsUI : MonoBehaviour
{
    public TextMeshProUGUI DaystoChristmasCount;
    public TextMeshProUGUI VaccineCount;
    public TextMeshProUGUI ReindeerPillCount;
    public TextMeshProUGUI CovidPillCount;
    public TextMeshProUGUI RoosterDecoyCount;

    // Start is called before the first frame update
    void Awake()
    {
        DaystoChristmasCount = this.transform.Find("Days to Christmas Count").GetComponent<TextMeshProUGUI>();
        VaccineCount = this.transform.Find("Vaccine Count").GetComponent<TextMeshProUGUI>();
        ReindeerPillCount = this.transform.Find("Reindeer Pill Count").GetComponent<TextMeshProUGUI>();
        CovidPillCount = this.transform.Find("COVID Pill Count").GetComponent<TextMeshProUGUI>();
        RoosterDecoyCount = this.transform.Find("Rooster Decoy Count").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        DaystoChristmasCount.text = Supplies.instance.remainingDays.ToString();
        VaccineCount.text = Supplies.instance.vaccines.ToString();
        ReindeerPillCount.text = Supplies.instance.reindeerPills.ToString();
        CovidPillCount.text = Supplies.instance.covidPills.ToString();
        RoosterDecoyCount.text = Supplies.instance.roosterDecoys.ToString();
    }
}
