[33mcommit c70b76159b872c29e29c519f62dccc89dc2f66c0[m[33m ([m[1;36mHEAD -> [m[1;32mdevelop[m[33m, [m[1;31morigin/develop[m[33m)[m
Author: Ali Riza Esenli <aliriza.esenli@hotmail.com>
Date:   Tue Jun 23 13:51:51 2020 +0200

    fixed project completion date display

[1mdiff --git a/Bugtracker/ClientApp/src/components/Projects.vue b/Bugtracker/ClientApp/src/components/Projects.vue[m
[1mindex 75df665..66533ba 100644[m
[1m--- a/Bugtracker/ClientApp/src/components/Projects.vue[m
[1m+++ b/Bugtracker/ClientApp/src/components/Projects.vue[m
[36m@@ -170,8 +170,10 @@[m [mexport default {[m
             this.createProject({[m
                 name: this.projectName,[m
                 description: this.projectDescription,[m
[31m-                completions: this.completionDate[m
[32m+[m[32m                completion: this.completionDate[m
             });[m
[32m+[m
[32m+[m
             [m
             this.$refs['new-project'].hide()[m
         }[m
[1mdiff --git a/Bugtracker/ClientApp/src/store/modules/projectDetails.js b/Bugtracker/ClientApp/src/store/modules/projectDetails.js[m
[1mindex 60ac275..a5d1d0f 100644[m
[1m--- a/Bugtracker/ClientApp/src/store/modules/projectDetails.js[m
[1m+++ b/Bugtracker/ClientApp/src/store/modules/projectDetails.js[m
[36m@@ -31,6 +31,7 @@[m [mconst actions = {[m
             }[m
         });[m
 [m
[32m+[m[32m        console.log(response.data)[m
         commit('setProjectDetails', response.data)[m
     },[m
 [m
[36m@@ -96,7 +97,7 @@[m [mconst mutations = {[m
         state.projectName = data.name,[m
         state.projectDescription = data.description,[m
         state.projectCreatedOn = data.createdOn,[m
[31m-        state.projectCompletion = data.compleation,[m
[32m+[m[32m        state.projectCompletion = data.completion,[m
         state.projectTickets = data.tickets[m
     },[m
     editProject: (state, data) => {[m
