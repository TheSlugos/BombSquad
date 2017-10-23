using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Xml;

namespace BombSquad
{
    class HighScores
    {
        public struct HighScore
        {
            public string Name;
            public string Score;
        }

        private const string Path = "BombSquadConfig.xml";
        private const string TableTagName = "HighScores";
        private const string LevelTagName = "Level";
        private const string NameTagName = "Name";
        private const string ScoreTagName = "Score";
        private const string EasyKeyName = "Easy";
        private const string MediumKeyName = "Medium";
        private const string HardKeyName = "Hard";
        private const string DummyScoreValue = "999";

        Dictionary<String, HighScore> _Scores;

        public string Easy
        {
            get
            {
                return String.Format("{0}: {1}\t{2}", EasyKeyName,
              EasyScore.Name, EasyScore.Score);
            }
        }

        public string Medium
        {
            get
            {
                return String.Format("{0}: {1}\t{2}", MediumKeyName,
              MediumScore.Name, MediumScore.Score);
            }
        }

        public string Hard
        {
            get
            {
                return String.Format("{0}: {1}\t{2}", HardKeyName,
              HardScore.Name, HardScore.Score);
            }
        }

        public HighScore EasyScore
        {
            get { return _Scores[EasyKeyName]; }
        }

        public HighScore MediumScore
        {
            get { return _Scores[MediumKeyName]; }
        }

        public HighScore HardScore
        {
            get { return _Scores[HardKeyName]; }
        }

        public HighScores()
        {
            _Scores = new Dictionary<string, HighScore>();

            HighScore easyScore;
            easyScore.Name = EasyKeyName;
            easyScore.Score = DummyScoreValue;
            _Scores.Add(EasyKeyName, easyScore);

            HighScore mediumScore;
            mediumScore.Name = MediumKeyName;
            mediumScore.Score = DummyScoreValue;
            _Scores.Add(MediumKeyName, mediumScore);

            HighScore hardScore;
            hardScore.Name = HardKeyName;
            hardScore.Score = DummyScoreValue;
            _Scores.Add(HardKeyName, hardScore);

            LoadHighScores();
        }

        private void LoadHighScores()
        {
            if (File.Exists(Path))
            {
                // read in high scores
                XmlDocument xml = new XmlDocument();
                xml.Load(Path);
                XmlNodeList nl = xml.GetElementsByTagName(TableTagName);
                foreach(XmlNode node in nl)
                {
                    XmlElement element = (XmlElement)node;
                    string level = element.GetElementsByTagName(LevelTagName)[0].InnerText;
                    string name = element.GetElementsByTagName(NameTagName)[0].InnerText;
                    string score = element.GetElementsByTagName(ScoreTagName)[0].InnerText;

                    newScore(level, name, score);
                }

                SaveHighScores();
            }
        }

        private void newScore(string level, string name, string score)
        {
            _Scores.Remove(level);
            HighScore newScore;
            newScore.Name = name;
            newScore.Score = score;
            _Scores.Add(level, newScore);
        }

        public void SaveHighScores()
        {
            DataTable table = new DataTable(TableTagName);
            DataColumn column = new DataColumn(LevelTagName);
            column.DataType = Type.GetType("System.String");
            table.Columns.Add(column);
            column = new DataColumn(NameTagName);
            column.DataType = Type.GetType("System.String");
            table.Columns.Add(column);
            column = new DataColumn(ScoreTagName);
            column.DataType = Type.GetType("System.String");
            table.Columns.Add(column);

            foreach(string key in _Scores.Keys)
            {
                DataRow row = table.NewRow();
                row[LevelTagName] = key;
                row[NameTagName] = _Scores[key].Name;
                row[ScoreTagName] = _Scores[key].Score;
                table.Rows.Add(row);
            }

            table.WriteXml(Path);
        }

        public void newEasyHighScore(string name, string score)
        {
            newScore(EasyKeyName, name, score);
        }

        public void newMediumHighScore(string name, string score)
        {
            newScore(MediumKeyName, name, score);
        }

        public void newHardHighScore(string name, string score)
        {
            newScore(HardKeyName, name, score);
        }
    }
}
