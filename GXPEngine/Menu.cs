using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;


namespace GXPEngine
{
    //----------------------------------------------------
    //                Menu : Sprite
    //----------------------------------------------------
    public class Menu : Sprite
    {

        //-------------- Sound Stuff --------------\\
        private Sound _menuMusic;
        private SoundChannel _menuMusicChannel;
        private Sound _levelMusic;
        private SoundChannel _musicCombatChannel;


        private bool _hasStarted = false;
        private Button[] _menuButtons = new Button[15];
        private string _levelSelected = "CityHeights.tmx";

        //-------------- Time Stuff --------------\\
        private int _lastKeyPressed;
        private int _KeyPressedTimeIntervalMS = 500;

        //-------------- Menu Stuff --------------\\
        private Boolean _mainMenuScreen = true;
        private Boolean _characterSelectionScreen = false;
        private Boolean _player1CharConfirm = false;
        private Boolean _player2CharConfirm = false;
        private string _chosenPlayer1Sprite;                //Important
        private string _chosenPlayer2Sprite;                //Important
        private Boolean _stageSelectionScreen = false;

        //-------------- Selection Stuff --------------\\
        private int _selectionCursor = 1;
        private int _selectionCursorPlayer1 = 1;
        private int _selectionCursorPlayer2 = 1;
        private int _minSelectionPosition = 0;
        private int _maxSelectionPosition = 1;

        private Button _screenBackground;
        private Button _screenBackground2;

        private Button[] _mapPictures = new Button[6];




        //------------------------------------------------
        //                  Menu()   
        //------------------------------------------------

        public Menu() : base("Main Menu.png")
        {


            _screenBackground = new Button("ChampSelectFinal.png");
            _screenBackground.SetOrigin(_screenBackground.width / 2, _screenBackground.height / 2);
            _screenBackground.x = (game.width / 2);
            _screenBackground.y = (game.height / 2);
            AddChild(_screenBackground);
            _screenBackground.visible = false;


            _screenBackground2 = new Button("MapSelect.png");
            _screenBackground2.SetOrigin(_screenBackground2.width / 2, _screenBackground2.height / 2);
            _screenBackground2.x = (game.width / 2);
            _screenBackground2.y = (game.height / 2);
            AddChild(_screenBackground2);
            _screenBackground2.visible = false;

            //---------------------------------- Main menu buttons ----------------------------------\\

            _menuButtons[0] = new Button("ExitHover.png");
            // _menuButtons[0].SetScaleXY(2.4f, 2.4f);
            _menuButtons[0].x = 100;
            _menuButtons[0].y = 400;
            AddChild(_menuButtons[0]);
            _menuButtons[1] = new Button("StartHover.png");
            // _menuButtons[1].SetScaleXY(2.4f, 2.4f);
            _menuButtons[1].x = 100;
            _menuButtons[1].y = 200;
            AddChild(_menuButtons[1]);
            _menuButtons[2] = new Button("ExitNormal.png");
            // _menuButtons[2].SetScaleXY(2.4f, 2.4f);
            _menuButtons[2].x = 100;
            _menuButtons[2].y = 400;
            AddChild(_menuButtons[2]);
            _menuButtons[3] = new Button("StartNormal.png");
            //  _menuButtons[3].SetScaleXY(2.4f, 2.4f);
            _menuButtons[3].x = 100;
            _menuButtons[3].y = 200;
            AddChild(_menuButtons[3]);


            for (int i = 0; i < _mapPictures.Length; i++)
            {
                // SetOrigin(_mapPictures[i].width / 2, _mapPictures[i].height / 2);

                _mapPictures[i] = new Button("Map" + i + ".png");
                _mapPictures[i].visible = false;
                AddChild(_mapPictures[i]);
                _mapPictures[i].SetOrigin(_mapPictures[i].width / 2, _mapPictures[i].height / 2);
            }
            //---------------------------------- Character selection buttons ----------------------------------\\

            _menuButtons[4] = new Button("1P up.png");
            _menuButtons[4].SetScaleXY(0.25f, 0.25f);
            _menuButtons[4].x = (game.width / 2 - _menuButtons[3].width - 68);  // 1.05f;
            _menuButtons[4].y = 0;
            AddChild(_menuButtons[4]);
            _menuButtons[4].visible = false;
            _menuButtons[5] = new Button("1P down.png");
            _menuButtons[5].SetScaleXY(0.25f, 0.25f);
            _menuButtons[5].x = (game.width / 2 - _menuButtons[3].width + 10);
            _menuButtons[5].y = (game.height / 2 - _menuButtons[3].height + 105);
            AddChild(_menuButtons[5]);
            _menuButtons[5].visible = false;
            _menuButtons[6] = new Button("2P up.png");
            _menuButtons[6].SetScaleXY(0.25f, 0.25f);
            _menuButtons[6].x = (game.width / 2 + _menuButtons[3].width - 138);  // 1.05f;
            _menuButtons[6].y = 0;
            AddChild(_menuButtons[6]);
            _menuButtons[6].visible = false;
            _menuButtons[7] = new Button("2P down.png");
            _menuButtons[7].SetScaleXY(0.25f, 0.25f);
            _menuButtons[7].x = (game.width / 2 + _menuButtons[3].width - 218);  // 1.05f;
            _menuButtons[7].y = (game.height / 2 + _menuButtons[3].height - 105);
            AddChild(_menuButtons[7]);
            _menuButtons[7].visible = false;
            _menuButtons[10] = new Button("1P Male.png");
            // _menuButtons[10].SetScaleXY(2.4f, 2.4f);
            _menuButtons[10].x = 0;
            _menuButtons[10].y = 0;
            AddChild(_menuButtons[10]);
            _menuButtons[10].visible = false;
            _menuButtons[11] = new Button("1P Female.png");
            //  _menuButtons[11].SetScaleXY(2.4f, 2.4f);
            _menuButtons[11].x = 0;
            _menuButtons[11].y = 0;
            AddChild(_menuButtons[11]);
            _menuButtons[11].visible = false;
            _menuButtons[12] = new Button("2P Female.png");
            //  _menuButtons[12].SetScaleXY(2.4f, 2.4f);
            _menuButtons[12].x = 0;
            _menuButtons[12].y = 0;
            AddChild(_menuButtons[12]);
            _menuButtons[12].visible = false;
            _menuButtons[13] = new Button("2P Male.png");
            //  _menuButtons[13].SetScaleXY(2.4f, 2.4f);
            _menuButtons[13].x = 0;
            _menuButtons[13].y = 0;
            AddChild(_menuButtons[13]);
            _menuButtons[13].visible = false;
            //---------------------------------- Stage selection buttons ----------------------------------\\
            _menuButtons[8] = new Button("Landscape1.png");
            _menuButtons[8].SetScaleXY(2.4f, 2.4f);
            _menuButtons[8].x = (game.width / 2 - _menuButtons[3].width - 100);
            _menuButtons[8].y = (game.height / 2 - _menuButtons[3].height - 100);
            AddChild(_menuButtons[8]);
            _menuButtons[8].visible = false;
            _menuButtons[9] = new Button("Landscape2.png");
            _menuButtons[9].SetScaleXY(2.4f, 2.4f);
            _menuButtons[9].x = (game.width / 2 - _menuButtons[3].width - 100);
            _menuButtons[9].y = (game.height / 2 - _menuButtons[3].height - 100);
            AddChild(_menuButtons[9]);
            _menuButtons[9].visible = false;



            _menuButtons[14] = new Button("square.png");
            //_menuButtons[14].SetOrigin(game.width, game.height);
            _menuButtons[14].x = (game.width / 2 - _menuButtons[3].width - 22);
            _menuButtons[14].y = (game.height / 2 - _menuButtons[3].height - 72);
            AddChild(_menuButtons[14]);
            _menuButtons[14].visible = false;


            //---------------------------------- Music loading ----------------------------------\\

            _levelMusic = new Sound("CombatMusic.mp3", true, true);
            _menuMusic = new Sound("MenuMusic.mp3", true, true);
            _menuMusicChannel = _menuMusic.Play();
            StartCombattMusic();
            StartMenuMusic();
        }

        //------------------------------------------------
        //                   Update()   
        //------------------------------------------------
        public void Update()
        {


            if (!_hasStarted)
            {

                //-------- Main menu menu --------\\
                if (_mainMenuScreen)
                {

                    HandleSelectionPosition();

                    if (Time.time > _lastKeyPressed + _KeyPressedTimeIntervalMS)
                    {
                        if (Input.GetKeyUp(Key.E))
                        {
                            //check what button is pressed to determine if what to do
                            if (_selectionCursor == 0)
                            {
                                Environment.Exit(1);
                            }
                            if (_selectionCursor == 1)
                            {
                                HideMenuScreen();
                                //_characterSelectionScreen = true;
                                _lastKeyPressed = Time.time;
                            }
                        }
                    }
                    //exit button
                    if (_selectionCursor == 0)
                    {
                        _menuButtons[2].visible = false;
                        _menuButtons[0].visible = true;
                    }
                    else
                    {
                        _menuButtons[2].visible = true;
                        _menuButtons[0].visible = false;
                    }
                    //start button
                    if (_selectionCursor == 1)
                    {
                        _menuButtons[3].visible = false;
                        _menuButtons[1].visible = true;
                    }
                    else
                    {
                        _menuButtons[3].visible = true;
                        _menuButtons[1].visible = false;
                    }
                }
                //-------- Character Selection menu --------\\
                if (_characterSelectionScreen)
                {
                    _screenBackground.visible = true;
                    HandleSelectionPositionPlayer1();

                    HandleSelectionPositionPlayer2();

                    if (Time.time > _lastKeyPressed + _KeyPressedTimeIntervalMS)
                    {
                        if (Input.GetKeyUp(Key.E))
                        {
                            //check what button is pressed to determine if what to do
                            if (_selectionCursorPlayer1 == 0)
                            {
                                _menuButtons[11].visible = true;
                                _menuButtons[4].visible = false;
                                _menuButtons[5].visible = false;

                                _chosenPlayer1Sprite = "Pob";
                                _player1CharConfirm = true;

                            }
                            if (_selectionCursorPlayer1 == 1)
                            {
                                _menuButtons[10].visible = true;
                                _menuButtons[4].visible = false;
                                _menuButtons[5].visible = false;

                                _chosenPlayer1Sprite = "Bob";
                                _player1CharConfirm = true;

                            }
                        }

                        if (Input.GetKeyUp(Key.O))
                        {

                            if (_selectionCursorPlayer2 == 0)
                            {
                                _menuButtons[13].visible = true;
                                _menuButtons[6].visible = true;
                                _menuButtons[7].visible = false;

                                _chosenPlayer2Sprite = "Bob";
                                _player2CharConfirm = true;
                            }
                            if (_selectionCursorPlayer2 == 1)
                            {
                                _menuButtons[12].visible = true;
                                _menuButtons[6].visible = false;
                                _menuButtons[7].visible = true;

                                _chosenPlayer2Sprite = "Pob";
                                _player2CharConfirm = true;

                            }
                        }
                        if (_player1CharConfirm && _player2CharConfirm)
                        {
                            HideCharSelectionScreen();
                            //. _showMenuScreen = 2;
                            _lastKeyPressed = Time.time;
                        }
                    }

                    //Character selection screen
                    if (_selectionCursorPlayer1 == 0)
                    {
                        _menuButtons[4].visible = false;
                        _menuButtons[5].visible = true;
                    }
                    else
                    {
                        _menuButtons[4].visible = true;
                        _menuButtons[5].visible = false;
                    }
                    if (_selectionCursorPlayer2 == 0)
                    {
                        _menuButtons[6].visible = false;
                        _menuButtons[7].visible = true;
                    }
                    else
                    {
                        _menuButtons[6].visible = true;
                        _menuButtons[7].visible = false;
                    }
                }

                //-------- Stage Selection menu --------\\

                if (_stageSelectionScreen)
                {





                    _screenBackground2.visible = true;
                    _menuButtons[14].visible = true;
                    HandleSelectionPosition();
                    _maxSelectionPosition = 4;
                    MapSelection();

                    // _menuButtons[14].visible = false;


                    if (Time.time > _lastKeyPressed + _KeyPressedTimeIntervalMS)
                    {
                        if (Input.GetKeyDown(Key.E))
                        {
                            //check what button is pressed to determine if what to do


                            for (int i = 0; i < 5; i++)
                            {
                                if (_selectionCursor == i)
                                {
                                    _menuMusicChannel.Stop();
                                    this.alpha = 0;
                                    _screenBackground2.visible = false;
                                    _menuButtons[14].visible = false;
                                    LoadLevel(_levelSelected);
                                    _hasStarted = true;
                                }
                            }

                        }
                    }

                    FancyStageSelection();
                }
            }
            //backspace returns to main menu
            if (Input.GetKey(Key.BACKSPACE))
            {
                DestroyLevel(_levelSelected);
                _hasStarted = false;
                this.alpha = 1;
                StartMenuMusic();
            }
            //restarts game
            if (Input.GetKey(Key.SPACE))
            {
                DestroyLevel(_levelSelected);
                _hasStarted = false;
                LoadLevel(_levelSelected);
            }

        }

        //------------------------------------------------
        //              HideMenuScreen()   
        //------------------------------------------------
        public void HideMenuScreen()
        {
            _characterSelectionScreen = true;
            _mainMenuScreen = false;
            for (int i = 0; i < 3; i++)
            {
                _menuButtons[i].alpha = 0;
            }
        }


        //------------------------------------------------
        //          HideCharSelectionScreen()   
        //------------------------------------------------
        public void HideCharSelectionScreen()
        {
            _screenBackground.visible = false;
            _stageSelectionScreen = true;
            _characterSelectionScreen = false;
            _selectionCursor = 0;
            for (int i = 4; i < 8; i++)
            {
                _menuButtons[i].alpha = 0;
            }

            for (int i = 10; i < 14; i++)
            {
                _menuButtons[i].alpha = 0;
            }
        }

        //------------------------------------------------
        //              LoadLevel()   
        //------------------------------------------------
        public void LoadLevel(string name)
        {
            //loads the actual game
            if (_hasStarted == false)
            {
                StartCombattMusic();
                //Destroys old level:
                List<GameObject> children = GetChildren();
                for (int i = children.Count - 1; i >= 0; i--)
                {
                    children[i].Destroy();
                }
                AddChild(new Level(name, _chosenPlayer1Sprite, _chosenPlayer2Sprite));
                _hasStarted = true;
            }
        }

        //------------------------------------------------
        //              DestroyLevel()   
        //------------------------------------------------
        public void DestroyLevel(string name)
        {
            //Destroys old level:
            List<GameObject> children = GetChildren();
            for (int i = children.Count - 1; i >= 0; i--)
            {
                children[i].Destroy();
            }
            scale = 1f;
        }

        //------------------------------------------------
        //              StartMenuMusic()   
        //------------------------------------------------
        public void StartMenuMusic()
        {
            _musicCombatChannel.Stop();
            _menuMusicChannel = _menuMusic.Play();
            _menuMusicChannel.Volume = 0.2f;
        }

        //------------------------------------------------
        //              StartMenuMusic()   
        //------------------------------------------------
        public void StartCombattMusic()
        {
            _menuMusicChannel.Stop();
            _musicCombatChannel = _levelMusic.Play();
            _musicCombatChannel.Volume = 0.1f;
        }

        //----------------------------------------------------
        //            HandleSelectionPosition()
        //----------------------------------------------------
        public void HandleSelectionPosition()
        {
            if (!_stageSelectionScreen)
            {
                if (Input.GetKeyDown(Key.W) && _selectionCursor < _maxSelectionPosition)
                {
                    _selectionCursor += 1;
                }
                if (Input.GetKeyDown(Key.S) && _selectionCursor > _minSelectionPosition)
                {
                    _selectionCursor -= 1;
                }
            }
            else
            {
                if (Input.GetKeyDown(Key.S) && _selectionCursor < _maxSelectionPosition)
                {
                    _selectionCursor += 1;
                }
                if (Input.GetKeyDown(Key.W) && _selectionCursor > _minSelectionPosition)
                {
                    _selectionCursor -= 1;
                }
            }
        }

        //----------------------------------------------------
        //         HandleSelectionPositionPlayer1()
        //----------------------------------------------------
        public void HandleSelectionPositionPlayer1()
        {
            if (!_player1CharConfirm)
            {
                if (Input.GetKey(Key.W) && _selectionCursorPlayer1 < _maxSelectionPosition)
                {
                    _selectionCursorPlayer1 += 1;
                }
                if (Input.GetKey(Key.S) && _selectionCursorPlayer1 > _minSelectionPosition)
                {
                    _selectionCursorPlayer1 -= 1;
                }
            }
        }

        //----------------------------------------------------
        //          HandleSelectionPositionPlayer2()
        //----------------------------------------------------
        public void HandleSelectionPositionPlayer2()
        {
            if (!_player2CharConfirm)
            {


                if (Input.GetKey(Key.I) && _selectionCursorPlayer2 < _maxSelectionPosition)
                {
                    _selectionCursorPlayer2 += 1;
                }
                if (Input.GetKey(Key.K) && _selectionCursorPlayer2 > _minSelectionPosition)
                {
                    _selectionCursorPlayer2 -= 1;
                }
            }
        }

        //----------------------------------------------------
        //             FancyStageSelection()
        //----------------------------------------------------
        public void FancyStageSelection()
        {
            if (_selectionCursor == _minSelectionPosition)
            {
                _mapPictures[_selectionCursor + 1].visible = true;
                _mapPictures[_selectionCursor + 1].SetXY(game.width / 2, game.height / 2 + 450);


                _mapPictures[_selectionCursor].visible = true;
                _mapPictures[_selectionCursor].SetXY(game.width / 2, game.height / 2);
            }



            if (_selectionCursor > _minSelectionPosition && _selectionCursor < _maxSelectionPosition)
            {
                if (!(_selectionCursor < 2))
                {

                    _mapPictures[1].visible = false;

                }

                _mapPictures[_selectionCursor - 1].visible = true;
                _mapPictures[_selectionCursor - 1].SetXY(game.width / 2, game.height / 2 - 450);


                _mapPictures[_selectionCursor].visible = true;
                _mapPictures[_selectionCursor].SetXY(game.width / 2, game.height / 2);


                _mapPictures[_selectionCursor + 1].visible = true;
                _mapPictures[_selectionCursor + 1].SetXY(game.width / 2, game.height / 2 + 450);


                if (_selectionCursor == 3 || _selectionCursor == 4)
                {
                    if (_selectionCursor == 3)
                    {
                        _mapPictures[4].visible = true;
                        _mapPictures[4].SetXY(game.width / 2, game.height / 2 + 450);
                    }

                    if (_selectionCursor == 4)
                    {
                        _mapPictures[4].visible = true;
                        _mapPictures[_selectionCursor].SetXY(game.width / 2, game.height / 2);
                    }

                }
                else
                {
                    _mapPictures[4].visible = false;
                }
            }
            if (_selectionCursor == _maxSelectionPosition)
            {
                _mapPictures[_selectionCursor].visible = true;
                _mapPictures[_selectionCursor].SetXY(game.width / 2, game.height / 2);

                _mapPictures[_selectionCursor - 1].visible = true;
                _mapPictures[_selectionCursor - 1].SetXY(game.width / 2, game.height / 2 - 450);

            }
            for (int i = _selectionCursor + 2; i < _maxSelectionPosition && _selectionCursor < _maxSelectionPosition - 2; i++)
            {

                _mapPictures[i].visible = false;

            }

            for (int i = _selectionCursor - 2; i < _minSelectionPosition && _selectionCursor > _minSelectionPosition + 2; i--)
            {

                _mapPictures[i].visible = false;

            }

        }

        //----------------------------------------------------
        //                 MapSelection()
        //----------------------------------------------------
        public void MapSelection()
        {

            if (_selectionCursor == 0)
            {
                _levelSelected = "Map0Downtown.tmx";

            }

            if (_selectionCursor == 1)
            {

                _levelSelected = "Map1TheTowers.tmx";

            }
            if (_selectionCursor == 2)
            {

                _levelSelected = "Map2Scaffolding.tmx";

            }
            if (_selectionCursor == 3)
            {


                _levelSelected = "Map3LastStand.tmx";
            }
            if (_selectionCursor == 4)
            {

                _levelSelected = "Map4TheLeap.tmx";

            }
        }


        //----------------------------------------------------
        //                 GetChosenPar1()
        //----------------------------------------------------
        public string GetChosenCharP1()
        {

            return _chosenPlayer1Sprite;

        }

        //----------------------------------------------------
        //                 GetChosenPar2()
        //----------------------------------------------------
        public string GetChosenCharP2()
        {
            return _chosenPlayer2Sprite;

        }


    }

}