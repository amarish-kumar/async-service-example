ASYNC/AWAIT DEMO
================

This repo helps to explain the difference between synchronous+asynchronous services.

It also shows the dangers/horrors of using TPL in a service.

There are a few branches here that are worth exploring.

You will need to point IIS at the service using the hostname `asyncservice`. 

**Don't forget to add a host file entry!**

**Edit the IIS App Pool to run with ~10 work processes!**

_This is necessary because the 'request execution limit' is set to 3/10/25 on desktop versions of Windows._

There is a `Vagrantfile` for running some load testing tools:

```
vagrant up
vagrant ssh
./attack-ab
./attack-siege
./attack-httperf
```

Pay particular attention to `parallel-and-tpl-walkthrough` vs `parallel-and-async-walkthrough`.

Monitor the locks+threads and concurrent requests in perfmon.
