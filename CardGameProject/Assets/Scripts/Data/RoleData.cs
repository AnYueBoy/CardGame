using System.Collections.Generic;
public class RoleData {

    public RoleType roleType;

    public List<int> cards;

    public int hp;

    public RoleData (RoleType roleType) {
        this.roleType = roleType;
        cards = new List<int> ();
        cards.Add (1);
        hp = 100;
    }

}