using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using NCMB;
using UnityEngine.SceneManagement;

//private InputField UserName;
//private InputField PassWord;

public class Loginsignin : MonoBehaviour
{
	public InputField UserName;
	public InputField PassWord;
	public InputField Index;
	


	public void Login ()
	{
		Debug.Log(UserName.text);
		Debug.Log(PassWord.text);

		//NCMBUserのインスタンス作成 
		NCMBUser user = new NCMBUser ();

		// ユーザー名とパスワードでログイン
		NCMBUser.LogInAsync (UserName.text, PassWord.text, (NCMBException e) => {    
			if (e != null) {
				UnityEngine.Debug.Log ("ログインに失敗: " + e.ErrorMessage);
			} else {
				UnityEngine.Debug.Log ("ログインに成功！");

				SceneManager.LoadScene("SelectMode_scene");

			}
		});

	}

	public void Signin ()
	{
		if(Index.text == "")
        {
			return;
        }

		Debug.Log(UserName.text);
		Debug.Log(PassWord.text);


		//NCMBUserのインスタンス作成 
		NCMBUser user = new NCMBUser ();
		
		//ユーザ名とパスワードの設定
		user.UserName = UserName.text;
		user.Password = PassWord.text;
		user.Add("mode", -1);
		user.Add("seed", -1);
		user.Add("ido", -1);
		user.Add("keido", -1);
		user.Add("Prefecture","");
		user.Add("Municipalities", "");

		user.Add("Pra_seed", -1);
		user.Add("Pra_ido", -1);
		user.Add("Pra_keido", -1);
		user.Add("Pra_Prefecture", "");
		user.Add("Pra_Municipalities", "");


		//会員登録を行う
		user.SignUpAsync ((NCMBException e) => { 
			if (e != null) {
				UnityEngine.Debug.Log ("新規登録に失敗: " + e.ErrorMessage);

				if(e.ErrorMessage == "userName is duplication.")
                {
					Login();
                }
			}
			else {
				UnityEngine.Debug.Log ("新規登録に成功");

				save(UserName.text, Index.text);

				Login();

				SceneManager.LoadScene("SelectMode_scene");

			}
		});

	}

	public void save(string name, string Index)
	{
		NCMBObject obj = new NCMBObject("Diary");
		obj["name"] = name;
		obj["Index"] = Int32.Parse(Index);
		obj["evaluation"] = "Z";
		obj.SaveAsync((NCMBException e) => {
			if (e != null)
			{
				//エラー処理
			}
			else
			{
				//成功時の処理
			}
		});

		NCMBObject obj2 = new NCMBObject("Pra_Diary");
		obj2["name"] = name;
		obj2["Index"] = Int32.Parse(Index);
		obj2["evaluation"] = "Z";
		obj2.SaveAsync((NCMBException e) => {
			if (e != null)
			{
				//エラー処理
			}
			else
			{
				//成功時の処理
			}
		});


	}



}
