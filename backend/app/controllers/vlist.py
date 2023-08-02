import json
from app.models import vlist_create
from flask import Blueprint, request, jsonify
from . import HttpStatus

bp = Blueprint('vlist', __name__, url_prefix="/vlist")


@bp.route("/", methods=['GET'])
def get():
    return "GET", HttpStatus.ok_200


@bp.route("/", methods=['POST'])
def post():
    try:
        vlist_create_obj = vlist_create.new_obj_from_dict(request.json)
        vlist_create_obj.validate()
        if not vlist_create_obj.is_valid():
            return (
                jsonify(vlist_create_obj.errors),
                HttpStatus.bad_request_400
            )

        return (
            "POST",
            HttpStatus.created_201
        )
    except Exception as e:
        print(e)
        return(
            jsonify({"error": "Something went wrong, please try again later."}),
            HttpStatus.internal_server_error_500
        )


@bp.route("/", methods=["PUT"])
def put():
    return "PUT"
