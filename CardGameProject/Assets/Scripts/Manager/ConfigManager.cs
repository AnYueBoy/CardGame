using System.Collections.Generic;
using LitJson;
using UFramework.Core;
using UFramework.GameCommon;
using UnityEngine;

public class ConfigManager : IConfigManager {
    private CardConfig cardConfig;
    public void Init () {
        this.loadAllConfig ();
    }

    private void loadAllConfig () {
        this.cardConfig = this.loadConfig<CardConfig> (ConfigUrl.cardUrl);
    }
    public CardData GetCardDataById (int id) {
        return this.cardConfig.GetCardDataById (id);
    }

    public List<CardData> GetCardDataByRoleType (RoleType roleType) {
        return this.cardConfig.GetCardDataByRoleType (roleType);
    }

    private T loadConfig<T> (string configUrl) where T : IConfig {
        TextAsset configContext = App.Make<IAssetsManager> ().GetAssetByUrlSync<TextAsset> (configUrl);
        string context = configContext.text;
        T configData = JsonMapper.ToObject<T> (context);
        configData.ConvertData ();
        return configData;
    }

}