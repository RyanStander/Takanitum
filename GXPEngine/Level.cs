using GXPEngine;
using System.Collections;
using TiledMapParser;
using System;

namespace GXPEngine
{



    //----------------------------------------------------
    //               Level : GameObject
    //----------------------------------------------------
    public class Level : GameObject
    {

        private Player _player;
        private Player _player2;


        //-------------- Screen Shake Stuff --------------\\
        private Boolean _player1Dead;
        private Boolean _player2Dead;
        private int _lastTimeUntillShake;
        private int _shakeTimeIntervalMS = 50;
        private float _speed = 10;
        private int _shakeCooldown = 0;

        //-------------- Tracking of the arrows --------------\\
        private ArrowTracking _trackerPlayer1;
        private ArrowTracking _trackerPlayer2;
        //-------------- Heads Up Display Stuff --------------\\
        private HUD _hud;
        private LivesCounter _livesCounter;
        private bool _gameOver = false;
        private HealthContainer _healthBars;


        private string _player1Sprite;
        private string _player2Sprite;

        //----------------------------------------------------
        //              Level(string filename)
        //----------------------------------------------------
        public Level(string filename, string p1Char, string p2Char)
        {

            _player1Sprite = p1Char;
            _player2Sprite = p2Char;

            //intialises all the objects
            Map leveldata = MapParser.ReadMap(filename);
            SpawnBackground(leveldata);
            SpawnObjects(leveldata);
            _healthBars = new HealthContainer(_player, _player2);
            AddChild(_healthBars);
            _hud = new HUD(_player, _player2);

                //_player
            AddChild(_hud);
            _livesCounter = new LivesCounter(_player, _player2,p1Char,p2Char);
            AddChild(_livesCounter);
            _trackerPlayer2 = new ArrowTracking(_player, _player2, 2, "Blue");
            AddChild(_trackerPlayer2);
            _trackerPlayer1 = new ArrowTracking(_player, _player2, 1, "Red");
            AddChild(_trackerPlayer1);

            
        }

        //-------------------------------------
        //          Update()
        //-------------------------------------
        public void Update()
        {
            ShakeyStuff();
            _gameOver = _hud.GetGameOver();

        }

        //----------------------------------------------------
        //          SpawnObjects(Map leveldata)
        //----------------------------------------------------
        public void SpawnObjects(Map leveldata)
        {
            //Spawns the object from the tiled file

            ObjectGroup tileGroup = leveldata.ObjectGroups[1];
            foreach (TiledObject obj in tileGroup.Objects)
            {
                Platform tile = new Platform("tileset3", obj.Height, obj.Width, obj.GetFloatProperty("floatX", 0), obj.X, obj.GetFloatProperty("floatY", 0), obj.Y, obj.GetFloatProperty("speedX", 2), obj.GetFloatProperty("speedY", 2));
                tile.SetFrame(obj.GID - 141);
                AddChild(tile);
            }
            if (leveldata.ObjectGroups == null || leveldata.ObjectGroups.Length == 0)
                return;
            ObjectGroup objectGroup = leveldata.ObjectGroups[0];
            if (objectGroup.Objects == null || objectGroup.Objects.Length == 0)
                return;
            foreach (TiledObject obj in objectGroup.Objects)
            {
                Sprite newObj = null;

                switch (obj.Name)
                {
                    case "Player1":
                        _player = new Player(obj.GetFloatProperty("speed", 10), obj.GetFloatProperty("jump", 10), _player1Sprite ,Key.W,Key.A,Key.D,Key.S,Key.E,Key.Q,Key.R);
                        newObj = _player;
                        break;
                    case "Player2":
                        _player2 = new Player(obj.GetFloatProperty("speed", 10), obj.GetFloatProperty("jump", 10), _player2Sprite ,Key.I, Key.J, Key.L, Key.K, Key.O, Key.U, Key.P);
                        newObj = _player2;
                        break;
                    case "Environment":
                        newObj = new Environmental(obj.Type, (int)obj.Width, (int)obj.Height);
                        break;




                }
                if (newObj != null)
                {
                    newObj.x = obj.X;
                    newObj.y = obj.Y;
                    newObj.rotation = obj.Rotation;
                    AddChild(newObj);
                }
            }
        }
        //----------------------------------------------------
        //          SpawnBackground(Map leveldata)
        //----------------------------------------------------
        public void SpawnBackground(Map leveldata)
        {
            if (leveldata.ImageLayers == null || leveldata.ImageLayers.Length == 0)
                return;
            ImageLayer imageLayer = leveldata.ImageLayers[0];
            ImgBackground imgBG = new ImgBackground(imageLayer.Image.FileName);
            AddChild(imgBG);
        }
        //-------------------------------------
        //          GameOver()
        //-------------------------------------
        public bool GameOver()
        {
            return _gameOver;
        }
        //-------------------------------------
        //          ShakeyStuff()
        //-------------------------------------
        public void ShakeyStuff()
        {
            if (_shakeCooldown != 0)
            {
                x = x + _speed;

                if (Time.time > _lastTimeUntillShake + _shakeTimeIntervalMS)
                {
                    _speed = _speed * -1;
                    _lastTimeUntillShake = Time.time;
                }
                _shakeCooldown = _shakeCooldown - 1;
            }

            _player1Dead = _player.GetHasDied();
            _player2Dead = _player2.GetHasDied();
            if (_player1Dead || _player2Dead)
            {
                _shakeCooldown = _shakeCooldown + 50;
            }
        }
    }
}