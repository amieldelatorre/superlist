from flask import Flask, request
from .config.config import Config
from .controllers import HttpStatus


def create_app():
    conf = Config()
    print(conf.hello())
    app = Flask(__name__)

    from .controllers import vlist
    app.register_blueprint(vlist.bp)

    @app.before_request
    def check_content_type_header():
        if not request.is_json:
            return {"error": "Header 'Content-Type' is not set to 'application/json'"}, HttpStatus.bad_request_400
        else:
            pass

    @app.after_request
    def set_content_type_header(response):
        response.headers['Content-Type'] = "application/json"
        return response

    @app.route("/health")
    def hello():
        return {"status": "alive"}, HttpStatus.ok_200

    return app
