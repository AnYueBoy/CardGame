using System.Collections.Generic;
public class RoleData {

    public RoleType roleType;

    public List<int> cards;

    public int hp;

    public int energy;

    public int maxEnergy;

    public int armor;

    public RoleData (RoleType roleType) {
        this.roleType = roleType;
        cards = new List<int> ();
        cards.Add (1);
        hp = 100;
        energy = maxEnergy = 4;
        armor = 0;
    }

}