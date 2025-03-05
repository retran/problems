package problem0729

import java.util.*

class MyCalendar {
    private data class Event(val from: Int, val to: Int)

    private val events = TreeSet<Event> { a, b -> a.from - b.from }

    fun book(startTime: Int, endTime: Int): Boolean {
        val event = Event(startTime, endTime)
        val prev = events.lower(event)
        val next = events.ceiling(event)

        if (prev != null && prev.to > event.from) {
            return false
        }

        if (next != null && next.from < event.to) {
            return false
        }

        events.add(event)
        return true
    }

}
