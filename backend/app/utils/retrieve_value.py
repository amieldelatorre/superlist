def get_val_from_dict_or_default(dictionary: dict[str, str], key: str, default: str) -> str:
    if key not in dictionary:
        return default
    return dictionary[key]
