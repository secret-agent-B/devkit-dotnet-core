# clean up
kubectl delete po,svc,deploy,ns -l app=logistics-app --namespace logistics-base-ns

# create namespace
kubectl create -f base/logistics-base-ns.yaml

# create deployments
kubectl create -f base/consul-deploy.yaml
kubectl create -f base/elasticsearch-deploy.yaml
kubectl create -f base/kibana-deploy.yaml
kubectl create -f base/mongo-deploy.yaml
kubectl create -f base/rabbitmq-deploy.yaml

# create services
kubectl create -f base/consul-service.yaml
kubectl create -f base/elasticsearch-service.yaml
kubectl create -f base/kibana-service.yaml
kubectl create -f base/mongo-service.yaml
kubectl create -f base/rabbitmq-service.yaml