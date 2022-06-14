# Problem Introduction

My job class is annotated DisallowConcurrentExecution attribute to avoid concurrence, so the same job just can run once per time (OK)

It is a CRON and runs every 1 minute (OK)

The time is 13:00:00s and the the job starts run (OK)

Now the time is 13:01:00s and the previous job didn't finish yet (OK)

As the class is annotated the next job will not start (IT IS GREAT - OK)

Now the time is 13:02:15s and the first job just finished (OK)

The job that was supposed to start at 13:02:00s is firing up at 13:02:15s (**Uh oh!!!**)

I dont want this to happen and instead this job (13:02:00s) must not run. I want the next job (13:03:00s) to run

References: [https://github.com/quartznet/quartznet/issues/1021](https://github.com/quartznet/quartznet/issues/1021)

# My Solution

* I stop using [DisallowConcurrentExecution] attribute.
* Instead, I make a object that will be locked when a job is run, and will be released when the job is finished.
* When a new job is run while a previous one has not been finished, the new run will try to get that object. If it cannot (due to the object still being locked), then the new run stops immediately without doing anything more.
