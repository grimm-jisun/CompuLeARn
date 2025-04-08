using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string questionText;       // Text of the question
        public string[] answers;         // Array of multiple-choice answers
        public int correctAnswerIndex;   // Index of the correct answer
    }

    public List<Question> questions;         // List to hold all predefined questions
    private List<Question> selectedQuestions; // Subset of randomly selected questions
    private int currentQuestionIndex = 0;    // Track the current question index
    private int score = 0;                   // Variable to track user's score

    // References to UI elements
    public TMP_Text questionText;            // TextMeshPro for the question text
    public TMP_Text scoreText;               // TextMeshPro for the Score text
    public TMP_Text warningText;             // TextMeshPro for the error message
    public GameObject passObject;            // GameObject for the "Pass" message
    public GameObject failObject;            // GameObject for the "Failed" message
    public Toggle[] answerToggles;           // Array for the answer toggles
    public GameObject questionPanel;         // Panel containing the question and answers UI
    public Button retryButton;               // Button for retrying the quiz
    public int questionLimit = 10;           // Limit for the number of questions

    void Start()
    {
        // Randomly select a subset of questions and initialize the quiz
        InitializeQuiz();

        // Ensure warning, Pass, and Failed objects are hidden at the start
        warningText.gameObject.SetActive(false);
        passObject.SetActive(false);
        failObject.SetActive(false);

        // Attach the retry button's functionality
        retryButton.onClick.AddListener(RetryQuiz);
    }

    // Method to shuffle the list and pick a subset
    private List<Question> GetRandomSubset(List<Question> originalList, int subsetSize)
    {
        System.Random random = new System.Random();
        List<Question> shuffledList = new List<Question>(originalList);

        // Shuffle the original list
        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            int j = random.Next(0, i + 1);
            Question temp = shuffledList[i];
            shuffledList[i] = shuffledList[j];
            shuffledList[j] = temp;
        }

        // Select the first `subsetSize` questions from the shuffled list
        return shuffledList.GetRange(0, Mathf.Min(subsetSize, shuffledList.Count));
    }

    // Triggered when the user clicks the SUBMIT button
    public void SubmitAnswer()
    {
        Toggle selectedToggle = GetActiveToggle();
        if (selectedToggle != null)
        {
            int selectedIndex = System.Array.IndexOf(answerToggles, selectedToggle);
            CheckAnswer(selectedIndex); // Pass the selected answer index
            warningText.gameObject.SetActive(false); // Hide the warning message
        }
        else
        {
            // Show warning message if no answer is selected
            warningText.text = "Please select an answer before proceeding!";
            warningText.gameObject.SetActive(true);

            // Start a coroutine to hide the warning after a delay
            StartCoroutine(HideWarningAfterDelay(3f)); // Hide after 3 seconds
        }
    }

    private Toggle GetActiveToggle()
    {
        foreach (var toggle in answerToggles)
        {
            if (toggle.isOn)
            {
                return toggle; // Return the active toggle
            }
        }
        return null; // No toggle selected
    }

    // Coroutine to hide the warning message after a delay
    private System.Collections.IEnumerator HideWarningAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        warningText.gameObject.SetActive(false);
    }

    // Check if the selected answer is correct
    private void CheckAnswer(int selectedAnswerIndex)
    {
        if (selectedAnswerIndex == selectedQuestions[currentQuestionIndex].correctAnswerIndex)
        {
            score++; // Increase the score for correct answers
            UpdateScoreText(); // Update the score text in the UI
        }

        currentQuestionIndex++;

        // Check if there are more questions or end the quiz
        if (currentQuestionIndex < selectedQuestions.Count)
        {
            UpdateQuestion();
        }
        else
        {
            EndQuiz();
        }
    }

    private void UpdateQuestion()
    {
        Question currentQuestion = selectedQuestions[currentQuestionIndex];

        // Set the question text
        questionText.text = currentQuestion.questionText;

        // Update the toggle text for each answer
        for (int i = 0; i < answerToggles.Length; i++)
        {
            if (i < currentQuestion.answers.Length)
            {
                answerToggles[i].GetComponentInChildren<TMP_Text>().text = currentQuestion.answers[i];
                answerToggles[i].isOn = false; // Reset toggle state
            }
        }
    }

    private void UpdateScoreText()
    {
        // Update the score text to display the current score
        scoreText.text = "Score: " + score.ToString();
    }

    private void EndQuiz()
    {
        Debug.Log("Quiz ended! Your score is: " + score);

        // Hide the question and answers UI
        questionPanel.SetActive(false);

        // Display Pass or Failed GameObject based on the score
        if (score >= 7)
        {
            passObject.SetActive(true);
        }
        else
        {
            failObject.SetActive(true);
        }

        scoreText.text = "Final Score: " + score.ToString(); // Display the final score
        scoreText.gameObject.SetActive(true);
    }

    private void RetryQuiz()
    {
        Debug.Log("Retrying the quiz!");

        // Reset score and index
        score = 0;
        currentQuestionIndex = 0;

        // Reinitialize questions
        selectedQuestions = GetRandomSubset(questions, questionLimit);

        // Hide Pass/Fail objects and re-show the question panel
        passObject.SetActive(false);
        failObject.SetActive(false);

        scoreText.gameObject.SetActive(false); // Hide final score
        questionPanel.SetActive(true);         // Show question panel

        // Start the quiz from the first question
        UpdateScoreText();
        UpdateQuestion();
    }

    private void InitializeQuiz()
    {
        // Randomize questions and start the quiz
        selectedQuestions = GetRandomSubset(questions, questionLimit);
        score = 0;
        currentQuestionIndex = 0;
        UpdateScoreText();
        UpdateQuestion();
    }
}
