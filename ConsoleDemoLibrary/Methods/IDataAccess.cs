﻿namespace ConsoleDemoLibrary.Methods
{
    public interface IDataAccess
    {
        void LoadData();
        void SaveData(string fileName);
    }
}