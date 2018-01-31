using Android.Content;
using Android.Preferences;
using System;
using System.Collections.Generic;
using System.Text;

namespace CamareiraFacil.Model
{
    public class AppPreferences
    {
        private ISharedPreferences nameSharedPrefs;
        private ISharedPreferencesEditor nameSharedEditor;
        private Context mContext;

        public AppPreferences(Context context)
        {
            this.mContext = context;
            nameSharedPrefs = PreferenceManager.GetDefaultSharedPreferences(mContext);
            nameSharedEditor = nameSharedPrefs.Edit();
        }

        public void saveAcessKey(string key, string value)
        {
            nameSharedEditor.PutString(key, value);
            nameSharedEditor.Apply();
        }

        public string getAcessKey(string key)
        {
            return nameSharedPrefs.GetString(key, "");
        }
    }
}
