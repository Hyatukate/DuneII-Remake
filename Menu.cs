using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace AccelCourse
{
    class Menu : Scene
    {
        Texture2D m_background1;
        Texture2D m_background2;
        Texture2D m_titleTexture;
        Texture2D m_subtitleTexture;
        Texture2D m_arrowSheet;
        Texture2D m_blueShipTexture, m_redShipTexture, m_greenShipTexture;
        Texture2D m_pixelFontSheet;
        Texture2D m_segaLogoSheet, m_logoTexture;

        SpriteFont m_standartFont;
        SpriteFont m_subTitleFont;
        SpriteFont m_copyrightFont;

        PixelFont m_standartPixelFont;

        SoundEffect m_titleMusic, m_menuSelect;

        GameObject m_arrow, m_menu, m_background, m_blueShip, m_redShip, m_greenShip, m_title, m_subtitle, m_titleMusicObject, m_segaLogo, m_logo, m_presents;

        public override void LoadConent(ContentManager _content)
        {
            m_background1 = _content.Load<Texture2D>("Menu\\Graphic\\intro1");
            m_background2 = _content.Load<Texture2D>("Menu\\Graphic\\intro2");
            m_arrowSheet = _content.Load<Texture2D>("Menu\\Graphic\\Arrow");
            m_blueShipTexture = _content.Load<Texture2D>("Menu\\Graphic\\blueShip");
            m_redShipTexture = _content.Load<Texture2D>("Menu\\Graphic\\redShip");
            m_greenShipTexture = _content.Load<Texture2D>("Menu\\Graphic\\greenShip");
            m_titleTexture = _content.Load<Texture2D>("Menu\\Graphic\\title");
            m_subtitleTexture = _content.Load<Texture2D>("Menu\\Graphic\\Subtitle");
            m_segaLogoSheet = _content.Load<Texture2D>("Menu\\Graphic\\SegaLogo");
            m_logoTexture = _content.Load<Texture2D>("Menu\\Graphic\\Logo");
            m_pixelFontSheet = _content.Load<Texture2D>("Menu\\Font\\Standart2");

            m_standartFont = _content.Load<SpriteFont>("Menu\\Font\\Standart");

            m_titleMusic = _content.Load<SoundEffect>("Menu\\Music\\Title");
            m_menuSelect = _content.Load<SoundEffect>("Menu\\Sound\\Menu Select");

            m_standartPixelFont = new PixelFont(new Spritesheet(m_pixelFontSheet, new Vector2(8, 8)));
        }

        public override void Initialize(out GameObject _root)
        {
            _root = new GameObject();
            _root.AddComponent(new EventController());
            
            m_background = new GameObject();
            m_background.transform.parent = _root.transform;
            _root.GetComponent<EventController>().AddEvent(new Event(m_background.AddComponent(new MenuBackground(m_background1, m_background2, 110f)), "Start", null, 5f));
            m_background.Initialize();

            m_arrow = new GameObject();
            m_arrow.AddComponent(new Animation(new Spritesheet(m_arrowSheet, 8), 0.125f));
            m_arrow.AddComponent(new MusicComponent(m_menuSelect));

            m_menu = new GameObject();
            m_menu.transform.parent = _root.transform;
            m_menu.transform.position += new Vector2(10f, 75f);
            m_menu.AddComponent(new UIMenu(new string[] { "Start Game", "Options", "Tutorial" }, Alignment.Left, 1f, Color.White, m_standartPixelFont, m_arrow));
            _root.GetComponent<EventController>().AddEvent(new Event(m_menu.AddComponent(new Fade(Color.TransparentBlack)), "NewFade", new object[] { Color.White, 1f }, 40f));
            m_menu.Initialize();
            
            m_blueShip = new GameObject();
            m_blueShip.transform.parent = _root.transform;
            m_blueShip.transform.position = new Vector2(-150, -200f);
            m_blueShip.active = false;
            m_blueShip.AddComponent(new SpriteRenderer(m_blueShipTexture));
            m_blueShip.AddComponent(new MoveObject(new Vector2(5f, -25), Vector2.Zero, (float)Math.PI / 6f, 3f));
            _root.GetComponent<EventController>().AddEvent(new Event(m_blueShip.AddComponent(new Activator()), "Activate", null, 27f));
            m_blueShip.Initialize();

            m_redShip = new GameObject();
            m_redShip.transform.parent = _root.transform;
            m_redShip.transform.position = new Vector2(0, 200f);
            m_redShip.active = false;
            m_redShip.AddComponent(new SpriteRenderer(m_redShipTexture));
            m_redShip.AddComponent(new MoveObject(new Vector2(25f, -10f), Vector2.Zero, (float)Math.PI / 6f, 3f));
            _root.GetComponent<EventController>().AddEvent(new Event(m_redShip.AddComponent(new Activator()), "Activate", null, 28.5f));
            m_redShip.Initialize();

            m_greenShip = new GameObject();
            m_greenShip.transform.parent = _root.transform;
            m_greenShip.transform.position = new Vector2(150f, -50f);
            m_greenShip.active = false;
            m_greenShip.AddComponent(new SpriteRenderer(m_greenShipTexture));
            m_greenShip.AddComponent(new MoveObject(new Vector2(20f, 30f), Vector2.Zero, (float)Math.PI / 6f, 2f));
            _root.GetComponent<EventController>().AddEvent(new Event(m_greenShip.AddComponent(new Activator()), "Activate", null, 31f));
            m_greenShip.Initialize();

            m_title = new GameObject();
            m_title.transform.parent = _root.transform;
            m_title.transform.position -= new Vector2(0f, 20f);
            m_title.AddComponent(new SpriteRenderer(m_titleTexture));
            _root.GetComponent<EventController>().AddEvent(new Event(m_title.AddComponent(new Fade(Color.TransparentBlack)), "NewFade", new object[] { Color.White, 1f }, 37f));
            m_title.Initialize();

            m_subtitle = new GameObject();
            m_subtitle.transform.parent = _root.transform;
            m_subtitle.transform.position += new Vector2(0f, 20f);
            m_subtitle.AddComponent(new SpriteRenderer(m_subtitleTexture));
            _root.GetComponent<EventController>().AddEvent(new Event(m_subtitle.AddComponent(new Fade(Color.TransparentBlack)), "NewFade", new object[] { Color.White, 1f }, 37f));
            m_subtitle.Initialize();

            m_titleMusicObject = new GameObject();
            m_titleMusicObject.transform.parent = _root.transform;
            _root.GetComponent<EventController>().AddEvent(new Event(m_titleMusicObject.AddComponent(new MusicComponent(m_titleMusic)), "Play", null, 4f));
            m_titleMusicObject.Initialize();

            m_segaLogo = new GameObject();
            m_segaLogo.transform.parent = _root.transform;
            m_segaLogo.transform.scale = new Vector2(0.181f, 0.181f);
            m_segaLogo.AddComponent(new Animation(new Spritesheet(m_segaLogoSheet, new Vector2(500, 165)), 0.0825f));
            _root.GetComponent<EventController>().AddEvent(new Event(m_segaLogo.AddComponent(new Fade(Color.White)), "NewFade", new object[] { Color.TransparentBlack, 0.5f }, 3.5f));
            _root.GetComponent<EventController>().AddEvent(new Event(m_segaLogo.AddComponent(new Activator()), "Deactivate", null, 4f));
            m_segaLogo.Initialize();

            m_logo = new GameObject();
            m_logo.transform.parent = _root.transform;
            m_logo.AddComponent(new SpriteRenderer(m_logoTexture));
            Fade logoFade = (Fade)m_logo.AddComponent(new Fade(Color.TransparentBlack));
            _root.GetComponent<EventController>().AddEvent(new Event(logoFade, "NewFade", new object[] { Color.White, 1f }, 9f));
            _root.GetComponent<EventController>().AddEvent(new Event(logoFade, "NewFade", new object[] { Color.TransparentBlack, 1f }, 15f));
            m_logo.Initialize();

            m_presents = new GameObject();
            m_presents.transform.parent = _root.transform;
            m_presents.AddComponent(new UIText("Presents", m_standartPixelFont, Color.White));
            Fade presentsFade = (Fade)m_presents.AddComponent(new Fade(Color.TransparentBlack));
            _root.GetComponent<EventController>().AddEvent(new Event(presentsFade, "NewFade", new object[] { Color.White, 1f }, 16f));
            _root.GetComponent<EventController>().AddEvent(new Event(presentsFade, "NewFade", new object[] { Color.TransparentBlack, 1f }, 22f));
            m_presents.Initialize();

            Game1.LoadScene("AccelCourse.Editor");
        }
    }
}
