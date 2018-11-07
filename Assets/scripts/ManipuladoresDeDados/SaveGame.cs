using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveGame
{
    public static void Save(DadosGlobais dadosG)
    {
#if !UNITY_N3DS
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(Application.persistentDataPath + "/touchBattle.arena");
        try
        {
            bf.Serialize(file, dadosG);
        }
        catch (System.SystemException e)
        {
            file.Close();
            Debug.LogError(e.StackTrace);
            Debug.Log("Serialgo falhou");
        }

        file.Close();
#else
        byte[] b = BytesTransform.ToBytes(dadosG);
        string s =JsonUtility.ToJson(new PreJSON() { b = b });
        PlayerPrefs.SetString("tapAttackSave", s);
        PlayerPrefs.Save();
#endif
    }

    public static DadosGlobais Load()
    {
        DadosGlobais retorno = new DadosGlobais();

#if !UNITY_N3DS
        try
        {
            
            if (File.Exists(Application.persistentDataPath + "/touchBattle.arena"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/touchBattle.arena", FileMode.Open);

                try
                {
                    retorno = (DadosGlobais)bf.Deserialize(file);
                    file.Close();
                }
                catch (System.SystemException e)
                {
                    file.Close();
                    Debug.Log("pARECE UM ERRO DE ALTERAÇÃO NO ARQUIVO DE sAVE");
                    Debug.LogError(e.StackTrace);
                    /*File.Delete(Application.persistentDataPath + "/touchBattle.arena");*/
                    retorno = new DadosGlobais();

                }

                

                return retorno;
            }
        }
        catch (IOException e)
        {
            Debug.Log("Deu um errinho OOOOOOOOOOOO");
            Debug.LogError(e.StackTrace);            
            return retorno;
        }
#else
        string prefs = PlayerPrefs.GetString("tapAttackSave");

        if (!string.IsNullOrEmpty(prefs))
        {
            PreJSON j = JsonUtility.FromJson<PreJSON>(prefs);
            retorno = BytesTransform.ToObject<DadosGlobais>(j.b);
        }
#endif

        return retorno;
    }
}


[System.Serializable]
public class PreJSON
{
    public byte[] b;
}
