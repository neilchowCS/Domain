using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EventLogger
{
    int i;
    private List<List<string>> log;

    public EventLogger(int i)
    {
        log = new();
        this.i = i;
    }

    public void AddLine(List<string> line)
    {
        log.Add(line);
    }

    public string FormatLine(List<string> line)
    {
        int length = 8;
        string output = "";
        while (line.Count < length)
        {
            line.Add("");
        }
        foreach (string entry in line)
        {
            output += (entry + ",");
        }
        return output;
    }

    public void WriteCSV()
    {
        if (i == 0)
        {
            using (StreamWriter w = new StreamWriter(Application.persistentDataPath + "/eventlogs0.csv"))
            {
                foreach (List<string> x in log)
                {
                    w.WriteLine(FormatLine(x));
                }
            }
        }
        else
        {
            using (StreamWriter w = new StreamWriter(Application.persistentDataPath + "/eventlogs1.csv"))
            {
                foreach (List<string> x in log)
                {
                    w.WriteLine(FormatLine(x));
                }
            }
        }
    }

    public void AddInitialize(IBattleUnit unit) {
        log.Add(new() { "INITIALIZE", $"{unit.UnitData.baseData.unitName} ({unit.GlobalObjectId})", $"timeline {unit.Timeline}", $"speed {unit.ObjSpeed.Value}", $"tile ({unit.X}. {unit.Y})" });
    }

    public void AddTimeline(IBattleUnit unit, float initialTimeline, float timespan, float distance, float finalTimeline, bool isLeader)
    {
        string x = (isLeader)?"*":"";
        log.Add(new() { "ADVANCE", $"{x}{unit.UnitData.baseData.unitName} ({unit.GlobalObjectId})", "timeline "+initialTimeline, $"atkspd {unit.UnitData.unitAttackSpeed.Value}", "timespan " + timespan, "distance " + distance, "final " + finalTimeline });
    }

    public void AddMovement(IBattleUnit unit, int prevX, int prevY)
    {
        log.Add(new() { "UNIT MOVE", $"{unit.UnitData.baseData.unitName} ({unit.GlobalObjectId})", $"({prevX}. {prevY}) to ({unit.X}. {unit.Y})"});
    }

    public void AddAttack(IBattleUnit unit, IBattleUnit target, AbilityType abilityType)
    {
        string x = "";
        switch (abilityType)
        {
            case AbilityType.Basic:
                x = "Basic";
                break;
            case AbilityType.Skill:
                x = "Skill";
                break;
            case AbilityType.Passive:
                x = "Passive";
                break;
        }
        log.Add(new() { "UNIT ATTACK", $"{unit.UnitData.baseData.unitName} ({unit.GlobalObjectId})", x, $"{target.UnitData.baseData.unitName} ({target.GlobalObjectId})" });
    }

    public void DealDamage(IBattleObject source, int amount, IBattleUnit target, int hpi, int hpf, bool isCrit)
    {
        log.Add(new() { "DEAL DAMAGE", $"{source.ObjectName} ({source.GlobalObjectId})", (!isCrit?("amount " + amount):("CRIT " + amount)), $"{target.UnitData.baseData.unitName} ({target.GlobalObjectId})", "hpi "+ hpi, "hpf "+hpf });
    }

    public void UnitDeath(IBattleUnit unit)
    {
        log.Add(new(){"UNIT DEATH", $"{unit.UnitData.baseData.unitName} ({unit.GlobalObjectId})" });
    }

    public void AddVictory(int i)
    {
        log.Add(new() { $"PLAYER {i} VICTORY" });
        WriteCSV();
    }
}

