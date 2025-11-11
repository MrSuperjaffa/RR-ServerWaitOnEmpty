using GalaSoft.MvvmLight.Messaging;
using Game.Events;
using Network;
using UnityEngine;
using UnityModManagerNet;
using Debug = UnityEngine.Debug;

namespace ServerWaitOnEmpty
{
    public class PlayerWatch
    {
        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.OnToggle = OnToggle;
            return true;
        }

        static bool OnToggle(UnityModManager.ModEntry modEntry, bool enabled)
        {
            if (enabled)
            {
                Messenger.Default.Register<PlayersDidChange>(modEntry, OnPlayersDidChange);
                Messenger.Default.Register<TimeMinuteDidChange>(modEntry, OnTimeChanged);
                Messenger.Default.Register<MapDidLoadEvent>(modEntry, OnMapDidLoad);
                Debug.Log("Registered events");
            } else
            {
                Messenger.Default.Unregister<PlayersDidChange>(modEntry, OnPlayersDidChange);
               Messenger.Default.Unregister<TimeMinuteDidChange>(modEntry, OnTimeChanged);
                Messenger.Default.Unregister<MapDidLoadEvent>(modEntry, OnMapDidLoad);
                SetPaused(false);
                AudioListener.volume = 1f;
                Debug.Log("Unregistered events");
            }

            return true;
        }

        private static void OnPlayersDidChange(PlayersDidChange message)
        {
            Debug.Log("OnPlayersDidChange called");
            CheckStatus();
        }

        private static void OnTimeChanged(TimeMinuteDidChange message)
        {
            Debug.Log("OnTimeChanged called");
            CheckStatus();
        }

        private static void OnMapDidLoad(MapDidLoadEvent message)
        {
            Debug.Log("OnMapDidLoad called");
            CheckStatus();
        }

        private static void CheckStatus()
        {
            if (Multiplayer.Mode != ConnectionMode.MultiplayerServer)
            {
                Debug.Log("Client is not server host");
                return;
            }

            var manager = UnityEngine.Object.FindObjectOfType<Game.State.PlayersManager>();
            int count = 0;
            if (manager != null)
            {
                foreach (var p in manager.RemotePlayers)
                {
                    count++;
                }
                Debug.Log("Remote Players: " + count.ToString());
            }

            if (count <= 0)
            {
                SetPaused(true);
                Debug.Log("No Players - Game Paused");
                global::Console.Log("No Players - Game Paused");
            }
            else
            {
                SetPaused(false);
                Debug.Log("Unpausing");
                global::Console.Log("Unpausing");
            }
        }

        private static void SetPaused(bool paused)
        {
            Time.timeScale = paused ? 0f : 1f;
            AudioListener.volume = 0f;
        }
    }
}

