# Task 3

## Create a simple deployment

* Create a deployment
  * $kubectl apply -f .\first-deploy.yaml
* View deployment and it's content
  * $kubectl get deployment
  * $kubectl describe deployment first-deploy
  * $kubectl get replicaset
  * $kubectl describe replicaset first-deploy-replicaID
  * $kubectl get pod -o wide
  * $kubectl describe pod first-deploy-replicaID-podId
* Edit configuration, set replica count to 5
  * Update deployment
  * View deployment, replica, pods
* Talk about cordon, show k8s cluster capacities
* Delete a pod in deployment
* Scale down deployment
* Delete deployment
  * $kubectl delete -f .\first-deploy.yaml



