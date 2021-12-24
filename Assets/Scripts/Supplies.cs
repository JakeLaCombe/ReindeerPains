using System.Collections;
using System.Collections.Generic;

public class Supplies
{
    public static Supplies instance = new Supplies();
    public int vaccines;
    public int reindeerPills;
    public int covidPills;
    public int roosterDecoys;
    public int smokeTraps;
    public bool hasShotgun;
    public int remainingDays;
    public int vaccinatedAdults;

    private int vaccineCache;
    private int reindeerPillCache;
    private int covidPillsCache;
    private int roosterDecoysCache;
    private int smokeTrapsCache;
    private bool hasShotgunCache;
    public int vaccinatedAdultsCache;


    private Supplies()
    {
        vaccines = 0;
        reindeerPills = 0;
        covidPills = 0;
        roosterDecoys = 1;
        smokeTraps = 0;
        remainingDays = 14;
        vaccinatedAdults = 0;
    }

    public void Reset()
    {
        vaccines = 0;
        reindeerPills = 0;
        covidPills = 0;
        roosterDecoys = 0;
        smokeTraps = 0;
        hasShotgun = false;
        remainingDays = 14;
        vaccinatedAdults = 0;
    }

    public void CacheSupplies()
    {
        vaccineCache = vaccines;
        reindeerPillCache = reindeerPills;
        covidPillsCache = covidPills;
        roosterDecoysCache = roosterDecoys;
        smokeTrapsCache = smokeTraps;
        hasShotgunCache = hasShotgun;
        vaccinatedAdultsCache = vaccinatedAdults;
    }

    public void RestoreFromCache()
    {
        vaccines = vaccineCache;
        reindeerPills = reindeerPillCache;
        covidPills = covidPillsCache;
        roosterDecoys = roosterDecoysCache;
        smokeTraps = smokeTrapsCache;
        hasShotgun = hasShotgunCache;
        vaccinatedAdults = vaccinatedAdultsCache;
    }
}