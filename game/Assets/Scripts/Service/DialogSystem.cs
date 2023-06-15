using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
            message.Next = new Message("В эту ночь происходит ужасное. На поселение нападают духи, крушат все вокруг и забирают вас в плен.", FontStyles.Italic | FontStyles.Bold);
            message = message.Next;
            message.OnNext += () =>
            {
                SceneManager.LoadScene("First", LoadSceneMode.Single);
                Settings.PlayerIsLocked = false;
            };
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
            message = message.Next;
            message.OnNext = () =>
            {
                Settings.PlayerIsLocked = false;
                Settings.EnemiesIsPeaceful = false;
            };
            return parent;
        }

        public static Message GetThirdMessage(Transform enemyTransform)
        {
            var parent = new Message("На обрыве весит дух, может мне помочь ему");
            parent.OnNext = () =>
            {
                var player = GameManager.Instance.Player.transform;
                var direction = (player.position - enemyTransform.position).normalized;
                var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, lookRotation, 1);

                var distance = Vector3.Distance(enemyTransform.position, player.position);
                enemyTransform.position += direction * (distance - 1);

                var delta = enemyTransform.position.y - player.position.y;
                enemyTransform.position -= new Vector3(0, delta - 0.3f, 0);
            };
            var message = parent;
            message.Next = new Message("Дух: Зачем ты спас меня, человек? Ведь нет ни одной причины желать мне добра");
            message = message.Next;
            message.Next = new Message("любая жизнь для меня важна, тем более жизнь оказавшегося беспомощным в беде");
            message = message.Next;
            message.Next = new Message("Дух: Даже мой народ не хотел меня спасать. я болен от рождения, мои руки и ноги очень слабы, так что избавиться от лишнего рта для них только в радость");
            message = message.Next;
            message.Next = new Message("Каждый достоин жизни, ведь не только силой можно принести пользу миру");
            message = message.Next;
            message.Next = new Message("Дух: Наверное ты прав, возможно, мне стоит найти другой народ, который оценить мои способности достойно");
            message = message.Next;
            message.OnNext = () =>
            {
                Settings.PlayerIsLocked = false;
                Settings.EnemiesIsPeaceful = false;
            };
            return parent;
        }

        public static Message GetTest()
        {
            var score = 0;
            var parent = new Message("Вам нужно ответить на вопросы старейшин, чтобы доказать, что вы свой", FontStyles.Italic | FontStyles.Bold);
            var question = parent;

            question.Next = new Message("В чем состоит цель истинного мудреца?", new List<Message>()
            {
                new Message("В совершенствовании человеческой души", FontStyles.Bold)
                {
                    OnNext = () => score += 2
                },
                new Message("В совершении общественного блага")
                {
                    OnNext = () => score += 1
                },
                new Message("В достижении личных интересов")
            }, getNextFromChoice: false);
            question = question.Next;

            question.Next = new Message("Какая личность является слабой?", new List<Message>()
            {
                new Message("Которая не стремится улучшать себя и всех вокруг", FontStyles.Bold)
                {
                    OnNext = () => score += 2,
                },
                new Message("Которая не может понять всю возвышенность и красоту искусства")
                {
                    OnNext = () => score += 1
                },
                new Message("Которая не может удержать меч в руке")
            }, getNextFromChoice: false);
            question = question.Next;

            question.Next = new Message("В долгом пути запасы еды всех твоих товарищей закончились, но у тебя осталось некоторое количество. Как ты поступишь?", new List<Message>()
            {
                new Message("я разделю припасы со веми поровну, ведь все мы люди и можем ошибаться", FontStyles.Bold)
                {
                    OnNext = () => score += 2,
                },
                new Message("Сначала я посмеюсь над их нерассчетливостью, а затем выдам немного своих припасов, ведь это их ошибка")
                {
                    OnNext = () => score += 1
                },
                new Message("Никому не скажу, что у меня остались припасы и буду питаться в тайне от всех, в следующий раз будут лучше собираться в путь")
            }, getNextFromChoice: false);
            question = question.Next;

            question.Next = new Message("Что значит для тебя совесть?", new List<Message>()
            {
                new Message("Совесть неотьемлемая часть личности, как и все другие эмоции. Она помогает понять, правильный твой поступок или нет.", FontStyles.Bold)
                {
                    OnNext = () => score += 2,
                },
                new Message("Совесть нельзя слушать постоянно, ведь иногда человеку приходиться переступить через нормы морали")
                {
                    OnNext = () => score += 1,
                },
                new Message("Совесть есть проявление слабости личности, которая ограничивает себя в действиях и не дает человеку раскрыть свой потенциал.")
            }, getNextFromChoice: false);
            question = question.Next;

            question.Next = new Message("Какое значение в твоей жизни имеет искусство?", new List<Message>()
            {
                new Message("Искусство для меня настолько же важно, как жизнь и смерть. Великие произведения художников или камнерезов пробуждают во мне высшие эмоции и направляют на верный путь", FontStyles.Bold)
                {
                    OnNext = () => score += 2,
                },
                new Message("Искусство бессомненно важно для меня, но жить нужно в реальности, а не в выдумке")
                {
                    OnNext = () => score += 1,
                },
                new Message("Искусство - угода слабых, придумавших другие миры и живущих в них. Неспособные сделать реально важные дела они посвящают жизнь безделью")
            }, getNextFromChoice: false);
            question = question.Next;

            question.Next = new Message("Как мудрец должен обращаться с другими людьми?", new List<Message>()
            {
                new Message("Уважать всех и каждого, пусть даже кто-то желает тебе зла. В итоге добро всегда побеждает.", FontStyles.Bold)
                {
                    OnNext = () => score += 2,
                },
                new Message("Надо относиться к человеку также, как и он ко мне.")
                {
                    OnNext = () => score += 1,
                },
                new Message("Нужно на всех смотреть с высока, и тогда все будут тебя бояться, а значит не сделают тебе ничего плохого")
            }, getNextFromChoice: false);
            question = question.Next;

            question.Next = new Message("eсли небо принесет тебе безграничные возможности, позволяющие тебе повелевать стихиями, что ты сделаешь с этой силой?", new List<Message>()
            {
                new Message("я откажусь от этой силы, ведь человек не сможет удержать ясность мысли при таком могуществе", FontStyles.Bold)
                {
                    OnNext = () =>
                    {
                        score += 2;
                        Debug.Log(score);
                        question.Next = GetAnswer(score);
                    }
                },
                new Message("я умножу богатсва нашего мира и принему процветание нашей цивилизации.")
                {
                    OnNext = () =>
                    {
                        score += 1;
                        Debug.Log(score);
                        question.Next = GetAnswer(score);
                    }
                },
                new Message("я уничтожу большинство людей в и буду править оставшимися, чтобы принести спокойствие и порядок в мир.")
                {
                    OnNext = () =>
                    {
                        Debug.Log(score);
                        question.Next = GetAnswer(score);
                    }
                }
            }, getNextFromChoice: false);
            question = question.Next;

            return parent;
        }

        private static Message GetAnswer(int score)
        {
            if (score >= 10)
            {
                return new Message("Мудрейшина: Мой друг, я рад, что ты смог выбраться из заточения этих упадших душой созданий. С помощью наших усилий мы восстановим священное место")
                {
                    OnNext = () =>
                    {
                        Debug.Log("Эпилог");
                        Settings.EnemiesIsPeaceful = false;
                    }
                };
            }
            else
            {
                return new Message("Мудрейшина: Твой взгляд на мир не похож на наш, мы не верим тебе, ступай прочь")
                {
                    OnNext = () =>
                    {
                        GameManager.Instance.ShowDeathScreen();
                        GameManager.Instance.PauseManager.SetPaused(true);
                        Settings.EnemiesIsPeaceful = false;
                    }
                };
            }
        }
    }
}