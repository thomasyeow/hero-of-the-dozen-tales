INCLUDE globals.ink

{
    - skeletonsQuest =="inProgress":
    ->questInProgress
    - skeletonsQuest =="questDone":
    ->questDone
    - skeletonsQuest=="questCanceled":
    ->questAgain
    - skeletonsQuest=="questReturned":
    ->questReturned
    - skeletonsQuest =="":
    ->main
}

EXTERNAL getQuest(title)
EXTERNAL completeQuest(title)


=== main ===
Those skeletons are guarding this entrance, but we have to get through here.

    ->main2


=== main2 ===
I've put up this barrier and I can destroy it if you agree to kill them.
->main3

=== main3 ===
~ getQuest("Skeleton extermination.")
->DONE

===questAgain===
Wanna kill them?
->questAgain2

===questAgain2===
~ getQuest("Skeleton extermination.")
->DONE

=== questInProgress === 
You need to kill those skeletons.
+[Ok]
->DONE

=== questDone === 
Good Job, here is your reward. Now, lets get out of here.
+[Ok]
->questDone2
=== questDone2 === 
~ completeQuest("Skeleton extermination.")
...
->DONE

===questReturned===
...
->DONE