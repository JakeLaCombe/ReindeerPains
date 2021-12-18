using System.Collections;
using System.Collections.Generic;

public class Supplies
{
    public static Supplies instance = new Supplies();
    public int vaccines;
    public int reindeerPills;
    public int covidPills;
    public int roosterDecoys;

    private Supplies()
    {
        vaccines = 0;
        reindeerPills = 0;
        covidPills = 0;
        roosterDecoys = 0;
    }

    public void Reset()
    {
        vaccines = 0;
        reindeerPills = 0;
        covidPills = 0;
        roosterDecoys = 0;
    }
}