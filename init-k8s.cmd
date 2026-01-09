@echo off

REM -- MongoDB
kubectl apply -f mongo-prep-service.yaml
kubectl apply -f mongo-prep-secrets.yaml
kubectl apply -f mongo-prep.yaml

REM -- App
kubectl apply -f app-prep-service.yaml
kubectl apply -f app-prep-ingress.yaml
kubectl apply -f app-prep.yaml

