﻿using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Askme.Domain
{
    [TestFixture]
    public class AnswerTest
    {
       
        [Test]
        public void AnswerKnowsWhenItWasCreated()
        {
            AskMeDate time = new AskMeDate();
            Answer answer = new Answer(time, null, "");
            Assert.AreEqual(time, answer.CreatedOn);
        }

        [Test]
        public void AnswerKnowsTheUserWhoAnswered()
        {
            const string userName = "PakodaSingh";
            User user = new User(userName, "******", "diptanu@thoughtworks.com");
            Answer answer = new Answer(new AskMeDate(), user, "");
            Assert.AreEqual(user, answer.User);
        }

        [Test]
        public void VerifyAnswerInsertion()
        {
            
            User user = new User("PakodaSingh", "******", "diptanu@thoughtworks.com");
            user.UserId = 1;
            Repository repository = Repository.GetInstance();
            repository.SaveUser(user);
            Answer answer = new Answer(new AskMeDate(),  user, "");
            repository.SaveAnswer(answer);
        }

        [Test]
        public void AnswerShouldHaveText()
        {
            const string answerText = "This was supposed to be a funny answer but Chandra couldn't come up with one";
            Answer answer = new Answer(new AskMeDate(), null, answerText);
            Assert.AreSame(answerText, answer.ToString());
        }

        [Test]
        public void IfUserAndDateAndTextAreSameThenAnswersAreEqual()
        {
            Answer answer1 = new Answer(new AskMeDate(), UserMother.Kamal, "this is good answer");
            Answer answer2 = new Answer(new AskMeDate(), UserMother.Kamal, "this is good answer");
            Assert.AreEqual(answer1, answer2);
        }

        [Test]
        public void ShouldBeAbleToSearchAnswersBasedOnAKeyword()
        {
            User user = UserMother.Kamal;

            Answer goodanswer = AnswerMother.KamalsGoodAnswer(user);
            Answer badanswer = AnswerMother.KamalsBadAnswer(user);
            string searchString = "good";
            Repository repository = Repository.GetInstance();
            repository.SaveUser(user);
            repository.SaveAnswer(goodanswer);
            repository.SaveAnswer(badanswer);
            IList<Answer> answersFound = repository.SearchKeyWordInAnswers(searchString);

            Assert.AreEqual(1, answersFound.Count);

        }
    }
}