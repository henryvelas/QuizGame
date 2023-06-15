// See https://aka.ms/new-console-template for more information
using QuizGame;
using System.Numerics;
using System;

string? player;


Console.WriteLine("/***** Quiz Game *****/");


var question = new List<Question>();
var answers = new List<Answer>();
var scores = new Dictionary<string, int>();

SeedQuestionAndOptions();
StartGame();

void StartGame()
{
    Console.WriteLine("");
    Console.WriteLine("/***** Welcome to the Game *****/");

    Console.WriteLine("What is you name?");
    player = Console.ReadLine();

    Console.WriteLine($"Great {player}, let's do this!");

    foreach (var item in question)
    {
        Console.WriteLine(item.QuestionText);
        Console.WriteLine("Please, enter 1, 2, 3 or 4");

        foreach (var option in item.Options)
        {
            Console.WriteLine($"{option.Id}.{option.Text}");
        }
        var answer = GetSelectedAnswer();
        AddAnswerToList(answer, item);
    }


    int score = GetScore();
    Console.WriteLine("");
    Console.WriteLine($"Nice try {player}, you answered well {score} questions...");

    UpdateScore(player, score);

    ShowScores();

    var answers = new List<Answer>();
    Console.WriteLine("");
    Console.WriteLine("Do you want to play againa?");
    Console.WriteLine("Enter yes to play again or any other key to stop...");
    var playAgain = Console.ReadLine().ToLower().Trim();

    if(playAgain == "yes")
        StartGame();
}

string GetSelectedAnswer()
{
    var answer = Console.ReadLine();

    if (answer != null && (answer == "1") || (answer == "2") || (answer == "3") || (answer == "4"))
        return answer;
    else
    {
        Console.WriteLine("That is not a valid option, please try again...");
        GetSelectedAnswer();
    }
    return answer;
}

void AddAnswerToList(string answer,Question question)
{
    answers.Add(new Answer
    {
        QuestionId = question.Id,
        SelectedOption = GetSelectedOption(answer, question)
    }) ;

}


Option GetSelectedOption(string answer, Question question)
{
    var selectedOption = new Option();

    foreach (var item in question.Options)
    {
        if (item.Id == int.Parse(answer))
            selectedOption= item;
    }

    return selectedOption;
}
void SeedQuestionAndOptions()
{
    question.Add(new Question
    {
        Id = 1,
        QuestionText = "What is the biggest country on earth?",
        Options = new List<Option>
        {
            new Option { Id= 1, Text="Australia"},
            new Option { Id= 2, Text="China"},
            new Option { Id= 3, Text="Russia", IsValid = true},
            new Option { Id= 4, Text="Canada"},
        }
    });

    question.Add(new Question
    {
        Id = 2,
        QuestionText = "What is the country whit the greatest population?",
        Options = new List<Option>
        {
            new Option { Id= 1, Text="India"},
            new Option { Id= 2, Text="China", IsValid=true},
            new Option { Id= 3, Text="Estados Unidos"},
            new Option { Id= 4, Text="Indonesia"},
        }
    });

    question.Add(new Question
    {
        Id = 3,
        QuestionText = "What was the less corrupt country in the world in 2021?",
        Options = new List<Option>
        {
            new Option { Id= 1, Text="Finlandia"},
            new Option { Id= 2, Text="New Zealand"},
            new Option { Id= 3, Text="Denmark", IsValid=true},
            new Option { Id= 4, Text="Norway"},
        }
    });

    question.Add(new Question
    {
        Id = 4,
        QuestionText = "What was the best country for quality of life in 2021?",
        Options = new List<Option>
        {
            new Option { Id= 1, Text="Norway", IsValid=true},
            new Option { Id= 2, Text="Belgium"},
            new Option { Id= 3, Text="Sweden"},
            new Option { Id= 4, Text="Switzerland"},
        }
    });
}

int GetScore()
{
    int score = 0;

    foreach (var item in answers)
    {
        if (item.SelectedOption.IsValid)
            score++;
    }
    return score;
}

void UpdateScore(string player, int score)
{
    bool update = false;
    foreach (var item in scores)
    {
        if(item.Key== player)
        {
            scores[item.Key] = score;
            update= true;
        }
        
    }
    if (!update)
    {
        scores.Add(player, score);
    }
}

void ShowScores()
{
    Console.WriteLine("");
    Console.WriteLine("SCORES: ");
    foreach (var item in scores)
    {
        Console.WriteLine($"{item.Key}, Scores: {item.Value}");

    }
}