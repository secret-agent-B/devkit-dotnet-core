# Create a config file for a 3 nodes cluster
cat << EOF > kind-devkit-3nodes.yaml
kind: Cluster
apiVersion: kind.x-k8s.io/v1alpha4
nodes:
  - role: control-plane
  - role: worker
  - role: worker
EOF

# Create a new cluster with the config file
kind create cluster --name devkit-cluster --config ./kind-devkit-3nodes.yaml

# Remove the file
rm -f kind-devkit-3nodes.yaml

# Check how many nodes it created
kubectl get nodes

# Check the services for the whole cluster
kubectl get all --all-namespaces