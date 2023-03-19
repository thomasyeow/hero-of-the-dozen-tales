VAR looping=0
->main


===main===
{looping>3:->alreadyDone|}
{looping>0: Anything else you want to know?|Oh, you're finally awake}
*[What happened?]
->main1
*[Where am I?]
->main2
*[How long was I knocked out?]
->main3
+{looping>0}[No.]
->ending

===main1===
We found you unconscious in the cave. Don't worry, I've already healed your wounds.
~ looping = looping+1
+[Oh, thanks.]
->main


===main2===
We found you wounded, so we took you to our village. I hope you don't mind.
~ looping = looping+1
+[No, thanks. You probably saved my life.]
->main

===main3===
Only half a day.
~ looping = looping+1
+[Well, thanks for taking care of me.]
->main

===ending===
~ looping = looping+1
Well, if that's all, you should talk to our Chief. He's over there, near that birdhouse.
+[Ok]
->DONE

===alreadyDone===
I have nothing more to tell you
->DONE

