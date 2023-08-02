from app.utils.retrieve_value import get_val_from_dict_or_default
from app.utils.validation import append_to_errors
from typing import List
from enum import StrEnum


class VListCreateAttributes(StrEnum):
    expiry = "expiry"
    created_by = "created_by"
    title = "title"
    passphrase = "passphrase"
    description = "description"


class VListCreate:
    expiry:         str
    created_by:     str
    title:          str
    passphrase:     str
    description:    str
    errors:         dict[str, List[str]]

    def __init__(self, expiry="", created_by="", title="", passphrase="", description=""):
        self.expiry = expiry.strip()
        self.created_by = created_by.strip()
        self.title = title.strip()
        self.passphrase = passphrase.strip()
        self.description = description.strip()
        self.errors = {}

    def validate(self) -> None:
        if self.created_by.strip() == "" or self.created_by.strip() is None:
            append_to_errors(key=str(VListCreateAttributes.created_by), message="Cannot be empty or null", errors=self.errors)
        if self.title.strip() == "" or self.title.strip() is None:
            append_to_errors(key=str(VListCreateAttributes.title), message="Cannot be empty or null", errors=self.errors)
        if self.passphrase.strip() == "" or self.passphrase.strip() is None:
            append_to_errors(key=str(VListCreateAttributes.passphrase), message="Cannot be empty or null", errors=self.errors)
        # Todo validate expiry

    def is_valid(self) -> bool:
        return len(self.errors) == 0


# Made the creation of a new object outside the constructor because there may be a use case in the future where
# the creation of the class is needed directly
def new_obj_from_dict(attributes: dict[str, str]) -> VListCreate:
    expiry = get_val_from_dict_or_default(dictionary=attributes, key=VListCreateAttributes.expiry, default="")
    created_by = get_val_from_dict_or_default(dictionary=attributes, key=VListCreateAttributes.created_by, default="Anon")
    title = get_val_from_dict_or_default(dictionary=attributes, key=VListCreateAttributes.title, default="")
    passphrase = get_val_from_dict_or_default(dictionary=attributes, key=VListCreateAttributes.passphrase, default="")
    description = get_val_from_dict_or_default(dictionary=attributes, key=VListCreateAttributes.description, default="")

    return VListCreate(
        expiry=expiry,
        created_by=created_by,
        title=title,
        passphrase=passphrase,
        description=description
    )
