package models

import (
	"time"
)

type SuperList struct {
	UUID        string
	DateCreated time.Time
	LastUpdated time.Time
	Description string
	SortOrder   int
}
