# Backend

```bash
# Run the backend application
# Requires python, pip and python-venv

python3 -v venv .venv
. .venv/bin/activate
pip install -r requiments.txt
waitress-serve --host 0.0.0.0 --port 5000 --call app:create_app
```