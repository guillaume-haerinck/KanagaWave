using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_LogicManager : MonoBehaviour {

    public float timingWindow = 0.15f;

    struct PlayerJump
    {
        public int player;
        public float time;

        public PlayerJump(int _player, float _time)
        {
            player = _player;
            time = _time;
        }
    }
    List<PlayerJump> leftTeamJumps;
    List<PlayerJump> rightTeamJumps;

    private static SC_LogicManager _instance;


    static public void registerJump(int player)
    {
        if(player < 8)
        {
            _instance.leftTeamJumps.Add(new PlayerJump(player, Time.timeSinceLevelLoad));
        }
        else
        {
            _instance.rightTeamJumps.Add(new PlayerJump(player, Time.timeSinceLevelLoad));
        }
    }


    void Awake ()
    {
        _instance = this;
        leftTeamJumps = new List<PlayerJump>();
        rightTeamJumps = new List<PlayerJump>();
    }
	
	void Update ()
    {
        float now = Time.timeSinceLevelLoad;
        int combo;
        float previousJumpTime;
        float jumpTime;

        // Left Team Combo Check.
        if(leftTeamJumps.Count > 0)
        {
            combo = 1;
            previousJumpTime = leftTeamJumps[0].time;
            for (int i = 1; i < leftTeamJumps.Count; i++)
            {
                jumpTime = leftTeamJumps[i].time;
               
                if(Mathf.Abs(jumpTime - previousJumpTime) > timingWindow)
                {
                    // If we missed the timing window, we remove all the previous combo times.
                    leftTeamJumps.RemoveRange(0, combo);
                    StartCoroutine(jumpImpactsCoroutine(combo, true));
                    combo = 1; // We set combo back to 1

                    i = 0;
                    previousJumpTime = leftTeamJumps[0].time; // At this point we are sure the list is not empty so we can safely access element 0.
                }
                else
                {
                    combo++;
                    previousJumpTime = jumpTime;
                }
            }

            jumpTime = now;
            if (Mathf.Abs(jumpTime - previousJumpTime) > timingWindow)
            {
                leftTeamJumps.Clear();
                StartCoroutine(jumpImpactsCoroutine(combo, true));
            }
        }


        // Right Team Combo Check.
        if (rightTeamJumps.Count > 0)
        {
            combo = 1;
            previousJumpTime = rightTeamJumps[0].time;
            for (int i = 1; i < rightTeamJumps.Count; i++)
            {
                jumpTime = rightTeamJumps[i].time;

                if (Mathf.Abs(jumpTime - previousJumpTime) > timingWindow)
                {
                    // If we missed the timing window, we remove all the previous combo times.
                    rightTeamJumps.RemoveRange(0, combo);
                    StartCoroutine(jumpImpactsCoroutine(combo, false));
                    combo = 1; // We set combo back to 1

                    i = 0;
                    previousJumpTime = rightTeamJumps[0].time; // At this point we are sure the list is not empty so we can safely access element 0.
                }
                else
                {
                    combo++;
                    previousJumpTime = jumpTime;
                }
            }

            jumpTime = now;
            if (Mathf.Abs(jumpTime - previousJumpTime) > timingWindow)
            {
                rightTeamJumps.Clear();
                StartCoroutine(jumpImpactsCoroutine(combo, false));
            }
        }

    }

    // TODO: calculate the medium time of the combo to offset totalJumpTime instead of using an arbitrary 0.05f;
    // Combo is the number of simultaneous jumps.
    private IEnumerator jumpImpactsCoroutine(int combo, bool isLeftTeam)
    {
        float totalJumpTime = SC_CharController.f_JumpAscTime + SC_CharController.f_JumpPauseTime + SC_CharController.f_JumpDscTime - 0.05f;

        yield return new WaitForSeconds(totalJumpTime / 2.0f);
        if (isLeftTeam)
        {
            if (combo >= 3)
            {
                SC_WaterManager.GenerateLeftBubbles(combo);
            }
            yield return new WaitForSeconds(totalJumpTime / 2.0f);

            SC_ShakeLeftIsland.Shake(combo);
            if (combo >= 3)
            {
				SC_UI_Manager.ShowYeahLeft();
				SC_WaterManager.GenerateWave(false, combo);
            }
        }
        else
        {
            if (combo >= 3)
            {
                SC_WaterManager.GenerateRightBubbles(combo);
            }
            yield return new WaitForSeconds(totalJumpTime / 2.0f);

            SC_ShakeRightIsland.Shake(combo);
            if (combo >= 3)
            {
				SC_UI_Manager.ShowYeahRight();
				SC_WaterManager.GenerateWave(true, combo);
            }
        }
    }
}
