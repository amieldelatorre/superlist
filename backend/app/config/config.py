import os


class Config:
    # SQLALCHEMY_DATABASE_URI = f"{db_type}://{db_username}:{db_password}@{db_host}:{db_port}/{db_name}"
    boo = os.environ.get("boo")

    def hello(self):
        return self.boo
