echo "Do you want to clean up existing deployments? (y/n)"
read clean

case $clean in
"y")
  kubectl delete po,svc,deploy,cm,pvc,pv,sc,secrets -l app=snappy --namespace devkit-ns
  ;;
esac

# Delete all 

# Setup the namespace
kubectl apply -f devkit-ns.yaml

# Create the storage class for Azure persistent volume claim
kubectl apply -f snappy-sc.yaml

# Execute the definitions
kubectl apply -f api-cache.yaml
kubectl apply -f chatr-db.yaml
kubectl apply -f consul.yaml
kubectl apply -f elasticsearch.yaml
kubectl apply -f kibana.yaml
kubectl apply -f rabbitmq.yaml
kubectl apply -f redisinsight.yaml

for x in {1..3}
do

cat << EOF > persistent-vol-$x.yaml 
apiVersion: v1
kind: PersistentVolume
metadata:
  namespace: devkit-ns
  name: persistentvol-$x
  labels:
    app: snappy
    name: persistentvol-$x
    
spec: 
  accessModes:
    - ReadWriteOnce
  capacity: 
    storage: 500Mi
  hostPath: 
    path: /tmp/data
EOF

kubectl create -f persistent-vol-$x.yaml 

rm persistent-vol-$x.yaml 

done
