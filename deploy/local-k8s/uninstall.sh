echo "To uninstall the services, pick one of the 3 options;"
echo "x - All services"
echo "b - Base services (database, service registry, elk)"
echo "a - Core services (file-store, gateway, security)"
read option

case $option in
"x")
  kubectl delete po,svc,deploy,ns -l app=logistics-app --namespace logistics-api-ns
  kubectl delete po,svc,deploy,ns -l app=logistics-app --namespace logistics-base-ns
  ;;
"b")
  kubectl delete po,svc,deploy,ns -l app=logistics-app --namespace logistics-base-ns
  ;;
"a")
  kubectl delete po,svc,deploy,ns -l app=logistics-app --namespace logistics-api-ns
  ;;
"x")
  echo "Invalid input!"
  ;;
esac