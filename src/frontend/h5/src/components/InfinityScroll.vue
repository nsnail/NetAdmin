<script setup>

let scrollY
const emit = defineEmits(['scrollBottom'])
let loading = false
window.addEventListener("scroll", () => {
    if (window.scrollY <= scrollY) {
        scrollY = window.scrollY;
        return;
    }
    scrollY = window.scrollY;
    if (
        (document.body.scrollHeight - window.scrollY) /
        window.innerHeight <
        2
    ) {
        if (!loading) {
            loading = true;
            emit('scrollBottom', () => {
                loading = false;
            })

        }
    }
});

</script>

<template>
</template>

<style scoped>
</style>