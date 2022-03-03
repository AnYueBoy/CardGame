using System.Collections.Generic;
public interface IConfigManager {

    void Init ();

    CardData GetCardDataById (int id);

    List<CardData> GetCardDataByRoleType (RoleType roleType);
}