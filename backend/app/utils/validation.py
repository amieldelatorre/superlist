from typing import List


def is_validate_date(date: str, errors: dict[str, List[str]]) -> None:
    pass


def append_to_errors(key: str, message: str, errors:  dict[str, List[str]]) -> None:
    if key not in errors:
        errors[key] = [message]
    else:
        errors[key].append(message)
