package models

import (
	"time"
)

type ListItemComment struct {
	DateCreated time.Time
	Author      string
	Comment     string
}
