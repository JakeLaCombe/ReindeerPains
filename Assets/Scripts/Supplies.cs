using System.Collections;
using System.Collections.Generic;

public class Supplies
{
    public static Supplies instance = new Supplies();
    public int vaccines;
    public int reindeerPills;
    public int covidPills;

    private Supplies()
    {
        vaccines = 0;
        reindeerPills = 0;
        covidPills = 0;
    }
}