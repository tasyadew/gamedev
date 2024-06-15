using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerContainer
{
    public PlayerInfo message;
    public string status;
}

[System.Serializable]
public class PlayerInfo
{
    public string first_name;
    public string last_name;
    public string alias;
    public string id;
    public string created;
    public List<Score> score;
    // public List<LearningModuleCompleted> learningModuleCompleted;
    // public List<QuizCompleted> quizCompleted;
    // public List<FullComplete> fullComplete;
}

[System.Serializable]
public class Score
{
    public string metric_id;
    public string metric_name;
    public string metric_type;
    public string value;
}

[System.Serializable]
public class LearningModuleCompleted
{
    public string metric_id;
    public string metric_name;
    public string metric_type;
    public string value;
}

[System.Serializable]
public class QuizCompleted
{
    public string metric_id;
    public string metric_name;
    public string metric_type;
    public string value;
}

[System.Serializable]
public class FullComplete
{
    public string metric_id;
    public string metric_name;
    public string metric_type;
    public string value;
}

[System.Serializable]
public class LearningModuleBadge
{
    public string metric_id;
    public string metric_name;
    public string metric_type;
    public string value;
}

[System.Serializable]
public class QuizBadge
{
    public string metric_id;
    public string metric_name;
    public string metric_type;
    public string value;
}

[System.Serializable]
public class FullCompleteBadge
{
    public string metric_id;
    public string metric_name;
    public string metric_type;
    public string value;
}

[System.Serializable]
public class LeaderboardContainer
{
    public LeaderboardList message;
    public string status;
}

[System.Serializable]
public class LeaderboardList
{
    public List<PlayerRanking> data;
}

[System.Serializable]
public class PlayerRanking
{
    public string alias;
    public string score;
    public int rank;
}

[System.Serializable]
public class MessageContainer
{
    public string message;
    public string status;
}