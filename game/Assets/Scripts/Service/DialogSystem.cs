using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Service
{
    internal static class DialogSystem
    {
        public static Message GetInitMessage()
        {
            var parent = new Message("Мудрец: Ты славно служишь нам - мудрейшинам этого храма. Всю жизнь мы посвятили себя любимому делу, ничто не отвлекает нас здесь. Никакое богатсво, никакие соблазны и прельщения...");
            var message = parent;
            message.Next = new Message("Дух: Но теперь настало и твое время постичь истинное понимание жизни. К сожалению, я не могу передать тебе его, сама жизнь предоставит возможность обрести это знание...");
            message = message.Next;
            message.Next = new Message("Дух: Но всегда помни, что только человек с большой душой и добротой ко всем сможет пройти этот путь...");
            message = message.Next;
            message.Next = new Message("В эту ночь происходит ужасное. На поселение нападают духи, крушат все вокруг и забирают главного героя в плен.", FontStyles.Italic | FontStyles.Bold);
            return parent;
        }

        public static Message GetFirstMessage()
        {
            var firstBranch = new Message("У меня нет  приказа, но прошу тебя, пожалуйста, отпусти меня", FontStyles.Bold)
            {
                Next = new Message("Дух: По уставу я должен задержать тебя, но никто не говорил со мной так уважительно как ты...")
            };
            var secondBranch = new Message("Пусти меня или тебе не поздоровится")
            {
                Next = new Message("Дух: Такими капризами меня не напугать")
            };

            var parent = new Message("Дух: Стой, ты не из нашего народа, покажи приказ о твоем освобождении", new List<Message>()
            {
                firstBranch,
                secondBranch
            });
            parent.Next = firstBranch.Next;

            secondBranch = secondBranch.Next;
            secondBranch.OnNext += () =>
            {
                GameManager.Instance.ShowDeathScreen();
                GameManager.Instance.PauseManager.SetPaused(true);
                Settings.PlayerIsLocked = false;
            };

            firstBranch = firstBranch.Next;
            firstBranch.Next = new Message("Дух: В ответ на твою доброту я отпущу тебя");
            firstBranch = firstBranch.Next;
            firstBranch.Next = new Message("Спасибо тебе, у тебя огромная душа");
            firstBranch = firstBranch.Next;
            firstBranch.Next = new Message("Дух: Зачем мне все эти завоевания, если одно слово для меня слаще всей жизни");
            firstBranch = firstBranch.Next;
            firstBranch.OnNext += () =>
            {
                SceneManager.LoadScene("Second", LoadSceneMode.Single);
                Settings.PlayerIsLocked = false;
            };
            return parent;
        }

        public static Message GetSecondMessage()
        {
            var parent = new Message("Вы немного солите суп духов. Внезапно в палатку заходит дух и замечает вас", FontStyles.Italic | FontStyles.Bold);
            var message = parent;
            message.Next = new Message("Дух: Что такое, кто ты еще такой? Отродок человека, что ты подмешал в нашу похлебку?");
            message = message.Next;
            message.Next = new Message("Это просто соль, она сделает вашу похлебку вкуснее");
            message = message.Next;
            message.Next = new Message("Дух: куснее? Мы едим чтобы набраться сил, а не для того, что нас кусали");
            message = message.Next;
            message.Next = new Message("Дух подходит к чану, пробует и замирает", FontStyles.Italic | FontStyles.Bold);
            message = message.Next;
            message.Next = new Message("Дух: Что...Что это такое, мое тело будто пробирает изнутри, так вот зачем нужна эта белая пыль, наш народ не настолько глуп, чтобы кидать грязь в еду, но глупые поступки иногда оказываются правильными");
            message = message.Next;
            message.Next = new Message("Дух: Иди, отродье человека, за такое открытие я пощажу тебя, пролезай в дыру в шатре, остальные тебя не заметят");
            message = message.Next;
            message.Next = new Message("Вы удивляетесь такому, но идёте дальше", FontStyles.Italic | FontStyles.Bold);
            return parent;
        }

        public static Message GetThirdMessage()
        {
            var parent = new Message("На обрыве весит дух, может мне помочь ему");
            var message = parent;
            message.Next = new Message("Дух: Зачем ты спас меня, человек? Ведь нет ни одной причины желать мне добра");
            message = message.Next;
            message.Next = new Message("Любая жизнь для меня важна, тем более жизнь оказавшегося беспомощным в беде");
            message = message.Next;
            message.Next = new Message("Дух: Даже мой народ не хотел меня спасать. я болен от рождения, мои руки и ноги очень слабы, так что избавиться от лишнего рта для них только в радость");
            message = message.Next;
            message.Next = new Message("Каждый достоин жизни, ведь не только силой можно принести пользу миру");
            message = message.Next;
            message.Next = new Message("Дух: Наверное ты прав, возможно, мне стоит найти другой народ, который оценить мои способности достойно");
            return parent;
        }
    }
}