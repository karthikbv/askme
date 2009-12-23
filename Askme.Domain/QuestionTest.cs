﻿using System;
using System.Collections.Generic;
using System.IO;
using NHibernate;
using NUnit.Framework;

namespace Askme.Domain
{
    [TestFixture]
    public class QuestionTest : NHibernateInMemoryBase
    {
       
        [Test]
        public void ShouldBeAbleToGetTheQuestionText()
        {
            string questionText = "What is the use of 'var' key word?";
            User user = new User("shanu","shanu","shanu@shanu.com");
            Question question = new Question(questionText,user);
            Assert.AreEqual(questionText,question.QuestionText);
        }

        [Test]
        public void ShouldBeAbleToCreateQuestionWithTag()
        {
            string questionText = "What is the use of 'var' key word?";
            Tag csharpTag = new Tag("C#");
            Tag javaTag = new Tag("Java");

            User user = new User("shanu", "shanu", "shanu@shanu.com");
            Question question = new Question(questionText,user);
            question.AddTags(csharpTag);
            question.AddTags(javaTag);

            Assert.AreEqual(csharpTag, question.GetTags.Tags[0]);
            Assert.AreEqual(javaTag, question.GetTags.Tags[1]);

        }

        [Test]
        public void ShouldCollectAnswers()
        {
            User user = new User("shanu", "shanu", "shanu@shanu.com");
            Question question = new Question("What is the use of 'var' key word?",user);

            question.AddAnswer(new Answer(new AskMeDate(), null, "first answer"));
            Assert.AreEqual(1, question.NumberOfAnswers);
            question.AddAnswer(new Answer(new AskMeDate(), null, "second answer"));
            Assert.AreEqual(2, question.NumberOfAnswers);
        }

        [Test]
        public void ShouldSaveAnswersToQuestion()
        {
            User user = UserMother.Kamal;
            Repository repository = Repository.GetInstance();
            repository.SaveUser(user);
            const string questionText = "What is the use of 'var' key word?";
            Question question = new Question(questionText,user);
            Answer ans = new Answer(new AskMeDate(), user, "this is bad answer");
            question.AddAnswer(ans);
            repository.SaveQuestion(question);
            IList<Answer> answers = repository.LoadAnswerForQuestion(question);
            Assert.AreEqual(1, answers.Count);
        }
    }
}