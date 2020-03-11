<template>
  <div>
    <b-calendar v-model="value" :date-disabled-fn="dateDisabled" locale="de" width="300px"></b-calendar>
  </div>
</template>

<script>
  export default {
    name: 'Date',
    data() {
      return {
        value: ''
      }
    },
    methods: {
      dateDisabled(ymd, date) {
        // Disable weekends (Sunday = `0`, Saturday = `6`) and
        // disable days that fall on the 13th of the month
        const weekday = date.getDay()
        // Return `true` if the date should be disabled
        return weekday === 0 || weekday === 6
      }
    }
  }
</script>

